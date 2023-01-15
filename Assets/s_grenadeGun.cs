using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_grenadeGun : s_weapon
{
	[SerializeField] GameObject m_grenadeShot;
	[SerializeField] float m_grenadeSpeed;
	[SerializeField] float m_grenadeForce;
	[SerializeField] float m_grenadeRadius;
	private GameObject m_currentGrenade;
	bool m_grenadeSwitch;

	override protected void Fire()
	{
		if (!m_grenadeSwitch)
		{
			Debug.Log("Grenade Attack");
			m_currentGrenade = Instantiate(m_grenadeShot, m_gunpoint.position, m_camera.transform.rotation);
			m_currentGrenade.GetComponent<Rigidbody>().AddForce(m_currentGrenade.transform.forward * m_grenadeSpeed);
			m_grenadeSwitch = true;
		}
		else
		{
			Debug.Log("Grenade Explode");
			m_rigidBody.AddExplosionForce(m_grenadeForce, m_currentGrenade.transform.position, m_grenadeRadius, 0f, ForceMode.Impulse);

			Destroy(m_currentGrenade);
			m_grenadeSwitch = false;
			m_currentGrenade = null;
		}
	}

}
