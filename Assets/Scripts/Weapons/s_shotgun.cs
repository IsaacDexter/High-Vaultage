using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_shotgun : s_weapon
{
    /// <summary>The gameobject projectile for the shotgun to fire. Should be set to p_shotgunShot</summary>
    [SerializeField] GameObject m_projectile;
    /// <summary>The number of pellets for the shotgun to fire in a hit</summary>
    [SerializeField] int m_projectileCount;
    /// <summary>The force the projectiles have on firing.</summary>
    [SerializeField] float projectileForce;
    /// <summary>How many seconds the projectile exists for before despawning</summary>
    [SerializeField] float m_projectileLifetime;

    /// <summary>The point to fire the projectiles from.</summary>
    private Transform m_firePoint;

    /// <summary>The force for the shotgun to knock the player back by.<summary>
    [SerializeField] float m_kickback;
    /// <summary>How densely packed the projectiles are when fired.</summary>
    [SerializeField] float m_spread;

	/// <summary>Gets the cameras forward vector and launch the player in the opposite direction</summary>
	override protected void Fire()
    {
        if (m_hand.m_charge >= m_chargeCost)    //if we can afford to fire...
        {
            m_hand.m_charge -= m_chargeCost;    //Pay the cost

            m_firePoint = gameObject.transform;         //Get the position of the gun to fire from
            for (int i = 0; i < m_projectileCount; i++) //For each projectile we're firing...
            {
                SpawnProjectile();                      //Spawn it
            }

            Kickback(); //Fire the plaYER BACKWARDS
        }
    }

    /// <summary>Calculates a random rotation, and instanciates a projectile, then transforms it according to the random rotation and the spread, then applies force and queues for deletion</summary>
    private void SpawnProjectile()
    {
        Quaternion angle = Random.rotation;                                                             //Calculate a random rotation
        GameObject shot = Instantiate(m_projectile, m_firePoint.position, m_camera.rotation);           //Instanciate a new projectile from the firing position using the cameras rotation
        shot.transform.rotation = Quaternion.RotateTowards(shot.transform.rotation, angle, m_spread);   //Rotate from the forward within maximum spread
        shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * projectileForce);              //apply the shot with its its force

        Destroy(shot, m_projectileLifetime);   //Destroy the projectile after its lifetime expires
    }

    /// <summary>Apply force to fire the player back upon firing the shotgun</summary>
    private void Kickback()
    {
        Vector3 direction = m_camera.forward;                               //Get the camera's forward vector.
        direction *= -1;                                                    //Flip it
        m_rigidBody.AddForce(direction * m_kickback, ForceMode.Impulse);    //Apply a force in the opposite direction the camera according to the shotguns kickback force

    }
}