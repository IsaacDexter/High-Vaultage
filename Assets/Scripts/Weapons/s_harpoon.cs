using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_harpoon : s_chargingWeapon
{
	[SerializeField] GameObject m_harpoonShot;
	[SerializeField] float m_harpoonSpeed;
	[SerializeField] float m_harpoonForce;
	[SerializeField] float m_harpoonReelSpeed;
	[SerializeField] float m_harpoonMinDistance;
	[SerializeField] float m_harpoonMaxDistance;
	private GameObject m_currentHarpoon=null;
	SpringJoint m_joint;
	bool m_harpoonCheck;
	private Transform m_firePoint;
	override protected void Fire()
	{
		if (m_currentHarpoon != null)
		{
			m_rigidBody.AddExplosionForce(-m_harpoonForce, m_currentHarpoon.transform.position, 9999, 2f, ForceMode.Impulse);
			Destroy(m_currentHarpoon);
			m_currentHarpoon = null;
			Destroy(m_joint);
			Debug.Log("Harpoon release");
			m_hand.m_charge -= m_chargeCost;
		}
	}

	protected override void Charge()
	{
		if (m_hand.m_charge >= m_chargeCost)
		{
			m_firePoint = gameObject.transform;
			if (m_currentHarpoon == null)
			{
				Debug.Log("Harpoon hold");
				m_currentHarpoon = Instantiate(m_harpoonShot, m_firePoint.position, m_camera.transform.rotation);
				m_currentHarpoon.GetComponent<s_harpoonShot>().m_owner = this;
				m_currentHarpoon.GetComponent<Rigidbody>().AddForce(m_currentHarpoon.transform.forward * m_harpoonSpeed);
			}
			if (m_joint != null)
			{
				m_joint.connectedAnchor = m_currentHarpoon.transform.position;
				if(m_joint.maxDistance> m_joint.minDistance)
				{
					m_joint.maxDistance -= m_harpoonReelSpeed * Time.deltaTime;
				}

			}
			base.Charge();
		}
	}
	public void AttachHarpoon()
	{
		Vector3 swingPoint = m_currentHarpoon.transform.position;
		m_joint = m_rigidBody.gameObject.AddComponent<SpringJoint>();
		m_joint.autoConfigureConnectedAnchor = false;
		m_joint.connectedAnchor = swingPoint;


		float distanceFromPoint = Vector3.Distance(gameObject.transform.position, swingPoint);

		m_joint.maxDistance = distanceFromPoint;
		m_joint.minDistance = m_harpoonMinDistance;

		m_joint.spring = 4.5f;
		m_joint.damper = 7f;
		m_joint.massScale = 4.5f;
	}
}
