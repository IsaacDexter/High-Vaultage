 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class s_revolver : s_chargingWeapon
{

	[SerializeField] float m_revolverDamage;
	float m_revolverCharge;
	[SerializeField] float m_revolverMaxDamage;

	/// <summary>The position to spawn projectiles at.</summary>
	private Transform m_firePoint;
	private bool m_canSwitch;
	List<GameObject> targets = new List<GameObject>();

	[Header("Projectile Settings")]
	/// <summary>The projectile to spawn when the weapon is fired.</summary>
	[SerializeField] private GameObject m_projectile;
	/// <summary>The lifetime in seconds before the projectile deletes itself.</summary>
	[SerializeField] private float m_projectileLifetime;
	/// <summary>The range in units of the beam.</summary>
	[SerializeField] private float m_range;

    [Header("Weapon Settings")]
	/// <summary>The speed for time to move at while the weapon is charging.</summary>
	[SerializeField] private float m_timeDilation;



	/// <summary></summary>
	override protected void Fire()
	{
		m_firePoint = gameObject.transform;	//Get the position to fire from
		Time.timeScale = 1f;							//Reset time dilation

		if (CheckCost())	//If we can afford to fire, pay the cost
		{
			//SpawnProjectile();	//Fire a projectile...
			CheckHit();			//...and check if it hit anything.
		}
		m_revolverCharge = m_revolverDamage;
	}

    public override void Cancel()
    {
        base.Cancel();
		Time.timeScale = 1f;
    }

    private void SpawnProjectile(Vector3 point)
    {
		m_audioSource.PlayOneShot(m_clip, m_volume); //Plays Firing SFX
		Debug.DrawRay(m_camera.position, m_camera.transform.forward * m_range, Color.green, 20);                                  //Draw a ray in the direction the weapon is pointing
		GameObject shotLineObject = Instantiate(m_projectile, m_firePoint.position, m_camera.rotation);   //Instanciate a shot facing in the direction of the camera

		LineRenderer shotline = shotLineObject.GetComponent<LineRenderer>();            //Get the shot's line
		shotline.SetPosition(0, m_firePoint.position);                                           //Set one end to the point the weapon fired from...
		//shotline.SetPosition(1, m_firePoint.position + (m_camera.transform.forward * m_range));  //... and the other to the end of the pistols range
		shotline.SetPosition(1, point);
		Destroy(shotLineObject, m_projectileLifetime);  //Queue the object for destruction
	}

	/// <summary>Checks to see if the weapon hit anything substantial, and calls hit on anything it did</summary>
	private void CheckHit()
	{
		RaycastHit[] hit = Physics.RaycastAll(m_camera.position, transform.parent.transform.forward, m_range);
		m_canSwitch = true;
		if (hit.Length > 0)    //Use  raycast to find the point where the gun hit.
		{    //If it hit anything...
			int furthestHit = 0;
			for (int i = 0; i < hit.Length; i++)
			{
				if (!targets.Contains(hit[i].transform.root.gameObject))
				{
					targets.Add(hit[i].transform.root.gameObject);
					if (hit[i].rigidbody != null)          //...and the hit object has a rigidbody...
					{
						Hit(hit[i].rigidbody.transform.root.gameObject);   //...call hit on that enemy
						if (hit[i].rigidbody.gameObject.transform.root.gameObject.layer != 8 && hit[i].rigidbody.gameObject.transform.root.gameObject.layer != 7)
						{
							furthestHit = i;
						}
					}
					else if (hit[i].transform.root.gameObject.tag == "Trigger" && m_canSwitch == true)
					{
						s_trigger button = hit[i].transform.root.gameObject.GetComponent<s_trigger>();
						button.Trigger();
						furthestHit = i;
						m_canSwitch = false;
					}
				}
			}
			targets.Clear();
			SpawnProjectile(hit[furthestHit].point);
		}
	}

	/// <summary>Checks if the hit object is an enemy, and if it is, destroys it</summary>
	/// <param name="hitObject">The object that was hit</param>
	private void Hit(GameObject hitObject)
    {
		Debug.Log("hit " + hitObject);
		if (hitObject.tag == "Enemy")        //if the hit object has the tag enemy...
		{
			Debug.Log("hit " + hitObject);

			hitObject.GetComponent<s_enemyHealth>().DamageEnemy(m_revolverCharge);			//...destroy the enemy
		}
	}

	/// <summary>Slows down time by m_timeDilation</summary>
	protected override void Charge()
	{
		if(m_revolverCharge <= m_revolverMaxDamage)
		{
			m_revolverCharge += 3*Time.deltaTime;
		}
		Time.timeScale = m_timeDilation;
		print("charging revolver... "+ m_revolverDamage);
		base.Charge();

	}

	/// <summary>Reenables gravity when released</summary>
	public override void Dequip()
	{
		Time.timeScale = 1f;
		base.Dequip();
	}
}
