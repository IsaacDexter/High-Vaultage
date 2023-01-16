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

	/// <summary>Sends the player upward according to cameras upwards vector with force proportional to the time spent charging</summary>
	/// 
	override protected void Fire()
	{
		m_firePoint = gameObject.transform;

		if (m_hand.m_charge >= m_chargeCost)
		{
			Time.timeScale = 1f;


			Debug.DrawRay(m_firePoint.position, transform.parent.transform.forward * m_revolverRange, Color.green, 20);
			GameObject shotLineObject = Instantiate(m_revolverShot, m_firePoint.position, Quaternion.Euler(transform.parent.transform.forward));
			LineRenderer shotline = shotLineObject.GetComponent<LineRenderer>();
			shotline.SetPosition(0, m_firePoint.position);
			shotline.SetPosition(1, transform.parent.transform.forward.normalized * m_revolverRange);
			Destroy(shotLineObject, 1);
			if (Physics.Raycast(m_firePoint.position, transform.parent.transform.forward, out m_hit, m_revolverRange))
			{
				if (m_hit.rigidbody != null)
				{
					GameObject hitEnemy = m_hit.rigidbody.transform.gameObject;
					Debug.Log("hit " + hitEnemy);
					if (hitEnemy.tag == "Enemy")
					{
						// call destroy on enemy
						Destroy(hitEnemy);
					}
				}
			}
		}
		else
		{
			Debug.Log("Slash");
		}
	}

	protected override void Charge(float elapsedTime)
	{
		base.Charge(elapsedTime);
		Time.timeScale = 0.25f;
		print("charging revolver...");
	}
}
