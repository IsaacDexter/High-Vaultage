using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_harpoon : s_chargingWeapon
{

	[SerializeField] float m_harpoonDamage;

	/// <summary>The currently fired harpoon, either in the air or swung off</summary>

	private GameObject m_currentHarpoon=null;
	/// <summary>The rope connecting the barrel of the gun to the stuck harpoon</summary>
	SpringJoint m_joint;

	/// <summary>The position to spawn projectiles at.</summary>
	private Transform m_firePoint;

    [Header ("Weapon Settings")]
	/// <summary>The force the player moves forward by when releasing</summary>
	[SerializeField] float m_releaseForce;

    [Header("Projectile Settings")]
	/// <summary>The projectile to fire (s_harpoonShot)</summary>
	[SerializeField] GameObject m_projectile;
	/// <summary>The Force to apply to the fired projectile</summary>
	[SerializeField] float m_projectileForce;

    [Header("Rope Settings")]
	/// <summary>How much rope to reel in per second</summary>
	[SerializeField] float m_reelSpeed;
	/// <summary>The minimum distance the rope can be</summary>
	[SerializeField] float m_ropeMinDistance;
	/// <summary>Strength of the sprint.</summary>
	[SerializeField] float m_ropeSpring;
	/// <summary>Amount the spring is reduced when active.</summary>
	[SerializeField] float m_ropeDamper;
	/// <summary>The scale to apply to the inverse mass and inertia tensor of the body prior to solving the constraints.</summary>
	[SerializeField] float m_ropeMassScale;

	/// <summary>If we're swinging on a harpoon, release it and move forwards.</summary>
	override protected void Fire()
	{
		if (m_currentHarpoon != null)	//If the harpoon is currently swining on a harpoon
		{
			if (m_joint != null)	//if we have a harpoon stuck to something
			{
				m_rigidBody.AddForce(m_camera.TransformDirection(Vector3.forward) * m_releaseForce, ForceMode.Impulse); //Apply a forward force to the player as you release
			}
			ReleaseHarpoon();	//Release that harpoon
		}
	}

    public override void Cancel()
    {
        base.Cancel();
		Fire();
    }

    /// <summary>Spawn the harpoon at m_firepoint, set its owner to be this, and add the initial force</summary>
    private void SpawnProjectile()
    {
		m_currentHarpoon = Instantiate(m_projectile, m_firePoint.position, m_camera.rotation);									//Spawn in the harpoon
		m_currentHarpoon.GetComponent<s_harpoonShot>().m_owner = this;													//Become its owner
		m_currentHarpoon.GetComponent<Rigidbody>().AddForce(m_currentHarpoon.transform.forward * m_projectileForce);	//Provide it with an initial force.
	}

	/// <summary>Releases our old harpoon and spawns a new one, if we can.</summary>
	public override void Press()
	{
		if (CheckCost())		//if we can afford to fire the harpoon...
		{
			m_firePoint = gameObject.transform;    //Set the fire point
			SpawnProjectile();  //Fire the harpoon
			base.Press();
		}
	}

	protected override void Charge()
	{
		if (m_joint != null)        //...and we have a joint to swing on
		{
			Reel();                 //Reel in on that point
		}
	}

	/// <summary>Check to see if the rope is not yet at it's minimum length, and reduces the roles length</summary>
	private void Reel()
    {
		if (m_joint.maxDistance > m_joint.minDistance)              //If the joints distance is not yet at its minimum...
		{
			m_joint.maxDistance -= m_reelSpeed * Time.deltaTime;    //Reel in the harpoon
		}
	}
	public void AttachHarpoon()
	{
		Vector3 swingPoint = m_currentHarpoon.transform.position;		//Get the position of the harpoon
		m_joint = m_rigidBody.gameObject.AddComponent<SpringJoint>();	//Add a joint to the player's rigid body
		m_joint.autoConfigureConnectedAnchor = false;					
		m_joint.connectedAnchor = swingPoint;							//Configure the joint's anchor to the harpoons position, as if we're swinging from it

		float distanceFromPoint = Vector3.Distance(gameObject.transform.position, swingPoint);	//Calculate the distance from the point...

		m_joint.maxDistance = distanceFromPoint;	//...and set it as the ropes length
		m_joint.minDistance = m_ropeMinDistance;		//Set the rope's minimum legth

		m_joint.spring = m_ropeSpring;		
		m_joint.damper = m_ropeDamper;		
		m_joint.massScale = m_ropeMassScale;	//Set the ropes properties
	}


	public void DamageTarget(GameObject target)
	{
		target.GetComponent<s_enemyHealth>().DamageEnemy(m_harpoonDamage); 
	}

	/// <summary>Destroys the current harpoon and joint.</summary>
	private void ReleaseHarpoon()
    {
		if (m_currentHarpoon != null)
		{
			Destroy(m_currentHarpoon.gameObject);
			m_currentHarpoon = null;    //Destroy the current harpoon...
			Destroy(m_joint);           //...and its joint
			m_joint=null;
		}
	}

    public override void Dequip()
    {
		ReleaseHarpoon();	//Destroy the harpoon that exists
        base.Dequip();		//Destroy this.
    }

}
