using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponWheel : MonoBehaviour
{
    /// <summary>If the weapon wheel is open at the moment.</summary>
    private bool m_open = false;
    /// <summary>The speed time should move at when the weapon wheel is open</summary>
    [SerializeField] private float m_timeDilation = 0.5f;
    [HideInInspector] public s_hand m_leftHand { get; private set; }
    [HideInInspector] public s_hand m_rightHand { get; private set; }
    private GameObject m_player;

private void Start()
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

    /// <summary>If the weapon wheel's open, close it and vice versa.</summary>
    public void Toggle()
    {
        if (m_open)
        {
            Close(); //If open, close
        }
        else
        {
            Open();  //If closed, open
        }
    }

    /// <summary>Closes the weapon wheel and locks the cursor</summary>
    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;     //Lock and hide the cursor
        m_open = false;  //Update check bool
        Time.timeScale = 1f;        //Speed time up to the normal amount
        gameObject.SetActive(false);
        m_player.GetComponent<s_playerInput>().m_acceptingInput = true;
    }

    /// <summary>Opens the weapons wheel, freeing the cursor and slowing down time</summary>
    public void Open()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;      //Unlock and show the cursor
        m_open = true;   //Update check bool
        Time.timeScale = m_timeDilation;      //Slow down time to the slow speed
        gameObject.SetActive(true);
        m_player.GetComponent<s_playerInput>().m_acceptingInput = false;
    }
}
