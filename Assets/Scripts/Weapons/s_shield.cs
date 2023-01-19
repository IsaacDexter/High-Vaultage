using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class s_shield : s_chargingWeapon
{
	[SerializeField] bool m_shielding = false;
	override protected void Fire()
	{
		m_rigidBody.useGravity = true;
		m_shielding = false;
	}

	protected override void Charge(float elapsedTime)
	{
		m_shielding=true;
		if (m_rigidBody.useGravity == true)
		{
			m_rigidBody.useGravity = false;
			m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
		}
		base.Charge(elapsedTime);
	}
}
