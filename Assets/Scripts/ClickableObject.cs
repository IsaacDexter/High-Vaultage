using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] e_weaponType m_weapon;
    [SerializeField] GameObject m_player;
    GunManager m_gunManager;
    void Start()
    {
        m_gunManager = m_player.GetComponent<GunManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left click");
            m_gunManager.SwitchWeapon(m_weapon, 1);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            m_gunManager.SwitchWeapon(m_weapon, 2);
        }

    }
}