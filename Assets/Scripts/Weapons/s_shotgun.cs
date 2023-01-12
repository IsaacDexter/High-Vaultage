using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_shotgun : s_weapon
{
    /// <summary>The force for the shotgun to knock the player back by</summary>
    [SerializeField] GameObject m_shotgunShot;
    [SerializeField] float m_shotgunPellets;
    [SerializeField] float m_shotgunSpread;
    [SerializeField] float m_shotgunSpeed;
    [SerializeField] float m_shotgunForce;

    /// <summary>Gets the cameras forward vector and launch the player in the opposite direction</summary>
    override protected void Fire()
    {
        //Vector3 direction = transform.parent.forward; //Get the players's forwards direction
        //m_rigidBody.AddForce(direction * -1.0f * m_force, ForceMode.Impulse);   //Use recoil to move the rigidbody back
        Debug.Log("Shogun");
        Vector3 direction = transform.parent.TransformDirection(Vector3.forward);
        //m_rigidBody.AddForce(new Vector3(100, 0, 0), ForceMode.Impulse);
        for (int i = 0; i < m_shotgunPellets; i++)
        {
            //Vector3 fireAngle = direction + new Vector3(Random.Range(-m_shotgunSpread, m_shotgunSpread), Random.Range(-m_shotgunSpread, m_shotgunSpread), Random.Range(-m_shotgunSpread, m_shotgunSpread));
            Quaternion angle = Random.rotation;
            GameObject shot = Instantiate(m_shotgunShot, gameObject.transform.position, transform.parent.rotation);
            shot.transform.rotation = Quaternion.RotateTowards(shot.transform.rotation, angle, m_shotgunSpread);
            shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * m_shotgunSpeed);
            Destroy(shot, 1);

        }
        direction *= -1;
        m_rigidBody.AddForce(direction * m_shotgunForce, ForceMode.Impulse);
    }
}