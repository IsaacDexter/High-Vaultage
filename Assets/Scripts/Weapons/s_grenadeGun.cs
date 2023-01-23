using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_grenadeGun : s_weapon
{

	[SerializeField] float m_grenadeDamage;

	s_grenadeShot m_grenadeScript;

    [Header("Projectile Settings")]
	/// <summary>The projectile to spawn m_grenadeShot</summary>
	[SerializeField] GameObject m_projectile;
	/// <summary>The intial force of fired projectiles.</summary>
	[SerializeField] float m_projectileForce;   


	/// <summary>The point on the gun to spawn the projectile</summary>
	private Vector3 m_firePoint;
	/// <summary>The currently active grenade, ready to be exploded</summary>
	private GameObject m_currentGrenade;

    [Header("Explosion Settings")]
	/// <summary>The strength of the force pulse of the explosion.</summary>
	[SerializeField] float m_explosionForce; 
	/// <summary>The radius of the force pulse of the explosion.</summary>
	[SerializeField] float m_explosionRadius;

	/// <summary>If there is currently a grenade sitting waiting to explode</summary>
	bool m_armed;

    #region Spawning

    /// <summary>If we don't have a grenade that exists, spawn a new one, so long as we can afford it. Otherwise, detonate our old one.</summary>
    override protected void Fire()
	{
		if (!m_armed)	//If theres not currently a grenade, armed...
		{
			m_firePoint = gameObject.transform.position;	//...Get the point to fire grenades from
			if (CheckCost())	//So long as we can afford it...
			{
				SpawnProjectile();	//Spawn and fire a grenade
			}
		}
		else	//If there is an armed grenade
		{
			DetonateProjectile();	//Detonate it
		}
	}

	/// <summary>Spawn m_currentGrenade, apply force to it and arm it.</summary>
	private void SpawnProjectile()
	{
		m_currentGrenade = Instantiate(m_projectile, m_firePoint, m_camera.transform.rotation);							//Spawn a new grenade
		m_currentGrenade.GetComponent<Rigidbody>().AddForce(m_currentGrenade.transform.forward * m_projectileForce);	//Fire it forwards
		m_grenadeScript = m_currentGrenade.GetComponent<s_grenadeShot>();
		m_armed = true;	//Indicate we are armed.
	}

    #endregion

    #region Detonating

    /// <summary>Add an impulse at the position of the grenade, and destroy the projectile</summary>
    private void DetonateProjectile()
    {
		if (m_currentGrenade != null)
		{
			//m_rigidBody.AddExplosionForce(m_explosionForce, m_currentGrenade.transform.position, m_explosionRadius, 0f, ForceMode.Impulse); //Apply a force. Note this will only affect the player at current.
			m_grenadeScript.Detonate(m_explosionForce, m_explosionRadius, m_grenadeDamage);
		}
		ReleaseProjectile();    //Destroy the grenade now its detonated.
	}

	/// <summary>Destroys the current grenade without exploding it. Called when dequipping.</summary>
	private void ReleaseProjectile()
	{
		if (m_currentGrenade != null)
		{
			Destroy(m_currentGrenade.gameObject);
			m_currentGrenade = null;    //Destroy the current grenade.
			m_armed = false;                //Show we have no armed grenade currently
		}
	}

    #endregion

	/// <summary>Destroy active grenades, then the weapon</summary>
    public override void Dequip()
    {
		ReleaseProjectile();
        base.Dequip();
    }
}
