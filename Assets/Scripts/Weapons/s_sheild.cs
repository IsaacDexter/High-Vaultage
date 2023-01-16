using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class s_sheild : s_chargingWeapon
{
	override protected void Fire()
	{
		m_rigidBody.useGravity = true;
	}

	protected override void Charge(float elapsedTime)
	{
		base.Charge(elapsedTime);

		if (m_rigidBody.useGravity == true)
		{
			m_rigidBody.useGravity = false;
			m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
		}
		print("Shield active...");
	}
}
