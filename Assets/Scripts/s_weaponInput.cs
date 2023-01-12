using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponInput : MonoBehaviour
{
    [SerializeField] s_weapon m_leftWeapon;
    [SerializeField] s_weapon m_rightWeapon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_leftWeapon.Press();
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_leftWeapon.Release();
        }
        if (Input.GetMouseButtonDown(1))
        {
            m_rightWeapon.Press();
        }
        if (Input.GetMouseButtonUp(1))
        {
            m_rightWeapon.Release();
        }
    }
}
