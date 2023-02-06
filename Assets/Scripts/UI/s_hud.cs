using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_hud : MonoBehaviour
{
    public s_hand m_leftHand;
    public s_hand m_rightHand;
    [SerializeField] private Image m_leftCharge;
    [SerializeField] private Image m_rightCharge;
    [SerializeField] Color m_leftColor = Color.green;
    [SerializeField] Color m_rightColor = Color.red;
    [SerializeField] Color m_flashColor = Color.white;
    [SerializeField] float m_flashSpeed = 2.0f;
    private GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = gameObject.transform.root.gameObject;
        s_hand[] hands = m_player.GetComponentsInChildren<s_hand>();    //Get the players hands
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
        m_leftCharge.color = m_leftColor;
        m_rightCharge.color = m_rightColor;
    }

    // Update is called once per frame
    void Update()
    {
        m_leftCharge.rectTransform.localScale = new Vector3(1, m_leftHand.m_charge);
        m_rightCharge.rectTransform.localScale = new Vector3(1, m_rightHand.m_charge);
        if (m_leftHand.m_regening && m_leftHand.m_charge < 1.0f)
        {
            m_leftCharge.color = Color.Lerp(m_flashColor, m_leftColor, Mathf.PingPong(Time.time * m_flashSpeed, 1.0f));
        }
        else
        {
            m_leftCharge.color = m_leftColor;
        }

        if (m_rightHand.m_regening && m_rightHand.m_charge < 1.0f)
        {
            m_rightCharge.color = Color.Lerp(m_flashColor, m_rightColor, Mathf.PingPong(Time.time * m_flashSpeed, 1.0f));
        }
        else
        {
            m_rightCharge.color = m_rightColor;
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
