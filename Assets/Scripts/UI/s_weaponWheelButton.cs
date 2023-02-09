using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class s_weaponWheelButton : s_clickableObject
{
    public GameObject m_weapon;
    s_weaponWheel m_weaponWheel;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        m_weaponWheel = GetComponentInParent<s_weaponWheel>();
    }

    protected override void LeftClick()
    {
        Equip(m_weaponWheel.m_leftHand, m_weaponWheel.m_rightHand);
    }

    protected override void RightClick()
    {
        Equip(m_weaponWheel.m_rightHand, m_weaponWheel.m_leftHand);
    }

    /// <summary>Swaps the weapons in our main and offhand if necessary, then equips the weapon to the mainhand.</summary>
    /// <param name="mainhand">The hand we are trying to equip the weapon to.</param>
    /// <param name="offhand">The hand we might have to swap the weapon with.</param>
    private void Equip(s_hand mainhand, s_hand offhand)
    {
        if (offhand.m_weapon != null)                               //If we have a weapon in our off hand...
        {
            if (offhand.m_weapon.GetComponent<s_weapon>().GetType() == m_weapon.GetComponent<s_weapon>().GetType())   //... and it's of the same class as the one we're trying to equip:
            {
                if (mainhand.m_weapon != null)          //If we have a weapon in our main hand...
                {
                    offhand.Equip(mainhand.m_weapon);   //...give it to the offhand
                }
                else                                    //If our mainhand is empty...
                {
                    offhand.Dequip();                   //...make our offhand empty too
                }
            }
        }
        mainhand.Equip(m_weapon);       //Then Equip this weapon to the mainhand
    }

    public void Unlock()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = true;
        }
    }

    public void Lock()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = false;
        }
    }
}
