using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class s_weaponWheelButton : s_clickableObject
{
    [SerializeField] GameObject m_weapon;
    s_weaponWheel m_weaponWheel;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        m_weaponWheel = GetComponentInParent<s_weaponWheel>();
    }

    protected override void LeftClick()
    {
        m_weaponWheel.m_leftHand.Equip(m_weapon);
    }

    protected override void RightClick()
    {
        m_weaponWheel.m_rightHand.Equip(m_weapon);
    }
}
