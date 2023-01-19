using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class s_revolver : s_chargingWeapon
{
	/// <summary>The position to spawn projectiles at.</summary>
	private Vector3 m_firePoint;

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
		m_firePoint = gameObject.transform.position;	//Get the position to fire from
		Time.timeScale = 1f;							//Reset time dilation

		if (CheckCost())	//If we can afford to fire, pay the cost
		{
			SpawnProjectile();	//Fire a projectile...
			CheckHit();			//...and check if it hit anything.
		}
	}

	private void SpawnProjectile()
    {
		Debug.DrawRay(m_firePoint, m_camera.transform.forward * m_range, Color.green, 20);                                  //Draw a ray in the direction the weapon is pointing
		GameObject shotLineObject = Instantiate(m_projectile, m_firePoint, m_camera.rotation);   //Instanciate a shot facing in the direction of the camera

		LineRenderer shotline = shotLineObject.GetComponent<LineRenderer>();            //Get the shot's line
		shotline.SetPosition(0, m_firePoint);                                           //Set one end to the point the weapon fired from...
		shotline.SetPosition(1, m_firePoint + (m_camera.transform.forward * m_range));  //... and the other to the end of the pistols range

		Destroy(shotLineObject, m_projectileLifetime);  //Queue the object for destruction
	}

	/// <summary>Checks to see if the weapon hit anything substantial, and calls hit on anything it did</summary>
	private void CheckHit()
    {
		RaycastHit hit;
		if (Physics.Raycast(m_firePoint, transform.parent.transform.forward, out hit, m_range))	//Use  raycast to find the point where the gun hit.
		{										//If it hit anything...
			if (hit.rigidbody != null)			//...and the hit object has a rigidbody...
			{
				Hit(hit.rigidbody.gameObject);	//...call hit on that enemy
			}
		}
	}

	/// <summary>Checks if the hit object is an enemy, and if it is, destroys it</summary>
	/// <param name="hitObject">The object that was hit</param>
	private void Hit(GameObject hitObject)
    {
		Debug.Log("hit " + hitObject);
		if (hitObject.tag == "Enemy")        //if the hit object has the tag enemy...
		{
			Destroy(hitObject);				//...destroy the enemy
		}
	}

	/// <summary>Slows down time by m_timeDilation</summary>
	protected override void Charge()
	{
		Time.timeScale = m_timeDilation;
		base.Charge();
	}
}
