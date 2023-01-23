 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class s_revolver : s_chargingWeapon
{
	private LineRenderer m_lineRenderer;
	private RaycastHit m_hit;
	[SerializeField] int m_revolverRange;
	[SerializeField] GameObject m_revolverShot;
	private Transform m_firePoint;
	[SerializeField] float m_revolverDammage;
	float m_revolverCharge;
	[SerializeField] float m_revolverMaxDammage;

	/// <summary>Sends the player upward according to cameras upwards vector with force proportional to the time spent charging</summary>
	/// 
	override protected void Fire()
	{
		m_firePoint = gameObject.transform;

		Time.timeScale = 1f;
		if (m_hand.m_charge >= m_chargeCost)
		{
			Debug.DrawRay(m_firePoint.position, m_camera.transform.forward * m_revolverRange, Color.green, 20);
			GameObject shotLineObject = Instantiate(m_revolverShot, m_firePoint.position, Quaternion.Euler(m_camera.transform.forward));
			LineRenderer shotline = shotLineObject.GetComponent<LineRenderer>();

			shotline.SetPosition(0, m_firePoint.position);
			shotline.SetPosition(1, m_firePoint.position+(m_camera.transform.forward * m_revolverRange));

			Destroy(shotLineObject, 1);
			if (Physics.Raycast(m_firePoint.position, transform.parent.transform.forward, out m_hit, m_revolverRange))
			{
				if (m_hit.rigidbody != null)
				{
					GameObject hitEnemy = m_hit.rigidbody.transform.root.gameObject;
					Debug.Log("hit " + hitEnemy);

					hitEnemy.GetComponent<s_enemyHealth>().DamageEnemy(m_revolverCharge);

				}
			}
			m_hand.m_charge -= m_chargeCost;
		}
		else
		{
			Debug.Log("Slash");
		}
		m_revolverCharge = m_revolverDammage;
	}

	protected override void Charge(float elapsedTime)
	{
		if(m_revolverCharge <= m_revolverMaxDammage)
		{
			m_revolverCharge += 3*Time.deltaTime;
		}
		Time.timeScale = 0.25f;
		print("charging revolver... "+ m_revolverDammage);
		base.Charge(elapsedTime);
	}
}
