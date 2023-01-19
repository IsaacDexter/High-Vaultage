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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left click");
            m_leftHand.Equip(m_weapon);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            m_rightHand.Equip(m_weapon);
        }

    }
}