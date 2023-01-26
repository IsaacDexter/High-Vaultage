using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_hud : MonoBehaviour
{
    public s_hand m_leftHand;
    public s_hand m_rightHand;
    [SerializeField] private Slider m_leftSlider;
    [SerializeField] private Slider m_rightSlider;
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
    }

    // Update is called once per frame
    void Update()
    {
        m_leftSlider.value = m_leftHand.m_charge;
        m_rightSlider.value = m_rightHand.m_charge;
    }
}
