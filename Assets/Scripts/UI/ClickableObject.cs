using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] GameObject m_weapon;
    [SerializeField] GameObject m_player;
    s_weaponManager m_weaponManager;
    void Start()
    {
        m_weaponManager = m_player.GetComponent<s_weaponManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left click");
            m_weaponManager.SwitchWeapon(m_weapon, 1);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            m_weaponManager.SwitchWeapon(m_weapon, 2);
        }

    }
}