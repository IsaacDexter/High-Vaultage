using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_menu : MonoBehaviour
{
    /// <summary>If the menu is open at the moment.</summary>
    public bool m_open = false;
    protected GameObject m_player;
    /// <summary>The speed time should move at when the weapon wheel is open</summary>
    [SerializeField] protected float m_timeDilation;

    virtual protected void Start()
    {
        m_open = false;
        m_player = gameObject.transform.root.gameObject;
        Close();
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
    virtual public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;     //Lock and hide the cursor
        m_open = false;  //Update check bool
        Time.timeScale = 1f;        //Speed time up to the normal amount
        gameObject.SetActive(false);
    }

    /// <summary>Opens the weapons wheel, freeing the cursor and slowing down time</summary>
    virtual public void Open()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;      //Unlock and show the cursor
        m_open = true;   //Update check bool
        Time.timeScale = m_timeDilation;      //Slow down time to the slow speed
        gameObject.SetActive(true);
    }
}
