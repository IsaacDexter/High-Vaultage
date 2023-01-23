using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] GameObject m_weapon;
    [SerializeField] GameObject m_player;
    s_hand m_leftHand;
    s_hand m_rightHand;
    void Start()
    {
        s_hand[] hands = m_player.GetComponentsInChildren<s_hand>();
        if (hands[0].name == "m_leftHand")
        {
            m_leftHand = hands[0];
            m_rightHand = hands[1];
        }
        else
        {
            m_leftHand = hands[1];
            m_rightHand = hands[0];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                m_leftHand.Equip(m_weapon);
                break;
            case PointerEventData.InputButton.Right:
                m_rightHand.Equip(m_weapon);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }
}