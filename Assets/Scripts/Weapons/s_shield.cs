using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class s_shield : s_chargingWeapon
{
	/// <summary>Reengages gravity and releases the shield</summary>
	override protected void Fire()
	{
		m_rigidBody.useGravity = true;	//Reenable gravity
	}

	/// <summary>Disengages gravity and resets the y velocity</summary>
    public override void Press()
    {
        base.Press();
		m_rigidBody.useGravity = false;
		m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
	}
}
