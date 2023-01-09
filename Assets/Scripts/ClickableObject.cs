using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
<<<<<<< HEAD
=======
    [SerializeField] e_weaponType m_weapon;
    [SerializeField] GameObject m_player;
    GunManager m_gunManager;
    void Start()
    {
        m_gunManager = m_player.GetComponent<GunManager>();
    }
>>>>>>> Chris

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
<<<<<<< HEAD
            Debug.Log("Left click");
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            Debug.Log("Right click");
=======
            m_gunManager.SwitchWeapon(m_weapon, 1);
        else if (eventData.button == PointerEventData.InputButton.Right)
            m_gunManager.SwitchWeapon(m_weapon, 2);
>>>>>>> Chris
    }
}