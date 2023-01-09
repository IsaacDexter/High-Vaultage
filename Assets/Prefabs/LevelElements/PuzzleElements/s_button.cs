using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_button : s_trigger
{
    /// <summary>How far to depress the button when its pressed</summary>
    protected float m_depressionDistance = -0.1f;

    /// <summary>A reference to the buttons plate</summary>
    [SerializeField] protected GameObject m_plate;

    /// <summary>The material to use while the button is not pressed</summary>
    [SerializeField] protected Material m_materialUnpressed;
    /// <summary>The material to switch to while the button is pressed</summary>
    [SerializeField] protected Material m_materialPressed;

    protected void SetPlateMaterial(Material material)
    {
        m_plate.GetComponent<MeshRenderer>().material = material;
    }

    override public void Trigger()
    {
        base.Trigger();
        Press();
    }

    public override void Detrigger()
    {
        base.Detrigger();
        Release();
    }

    protected void Press()
    {
        SetPlateMaterial(m_materialPressed);   //Switch to the pressed material
        m_plate.transform.position += m_plate.transform.up * m_depressionDistance;
    }

    protected void Release()
    {
        SetPlateMaterial(m_materialUnpressed);    //Switch to the unpressed material
        m_plate.transform.position += m_plate.transform.up * -m_depressionDistance;   //press the button by negative depression distance
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (CheckCollider(other))
        {
            Trigger();
        }
    }

    virtual protected bool CheckCollider(Collider other)
    {
        if (!m_triggered) //Check the button isn't already pressed
        {
            if (other.tag == "Player")  //If the player is the one colliding with it, press it
            {
                return true;
            }
        }
        return false;
    }

    protected void OnTriggerExit(Collider other)
    {
        if (CheckCollider(other))
        {
            Detrigger();
        }
    }
}
