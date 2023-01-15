using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_harpoon : s_chargingWeapon
{
	[SerializeField] GameObject m_harpoonShot;
	[SerializeField] float m_harpoonSpeed;
	[SerializeField] float m_harpoonForce;
	private GameObject m_currentHarpoon;
	SpringJoint m_joint;
	bool m_harpoonCheck;
	override protected void Fire()
	{
		m_rigidBody.AddExplosionForce(-m_harpoonForce, m_currentHarpoon.transform.position, 9999, 2f, ForceMode.Impulse);
		Destroy(m_currentHarpoon);
		m_currentHarpoon = null;
		Destroy(m_joint);
	}

	protected override void Charge(float elapsedTime)
	{
		base.Charge(elapsedTime);
		if (m_currentHarpoon != null)
		{
			Debug.Log("Harpoon Attack");
			//AttachHarpoon();
			m_currentHarpoon = Instantiate(m_harpoonShot, m_gunpoint.position, m_camera.transform.rotation);
			m_currentHarpoon.GetComponent<Rigidbody>().AddForce(m_currentHarpoon.transform.forward * m_harpoonSpeed);
			Vector3 swingPoint = m_currentHarpoon.transform.position;
			m_joint = gameObject.AddComponent<SpringJoint>();
			m_joint.autoConfigureConnectedAnchor = false;
			m_joint.connectedAnchor = swingPoint;

			float distanceFromPoint = Vector3.Distance(gameObject.transform.position, swingPoint);

			m_joint.maxDistance = distanceFromPoint * 0.8f;
			m_joint.minDistance = distanceFromPoint * 0.25f;

			m_joint.spring = 4.5f;
			m_joint.damper = 7f;
			m_joint.massScale = 4.5f;
		}

		m_joint.connectedAnchor = m_currentHarpoon.transform.position;
		print("Shield active...");
	}
}
