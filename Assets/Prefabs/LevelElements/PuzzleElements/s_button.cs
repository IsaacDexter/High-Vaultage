using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_button : s_trigger
{
    /// <summary>How far to depress the button when its pressed</summary>
    float m_depressionDistance = -0.1f;

    /// <summary>A reference to the buttons plate</summary>
    [SerializeField] private GameObject m_plate;

    /// <summary>The material to use while the button is not pressed</summary>
    [SerializeField] private Material m_materialUnpressed;
    /// <summary>The material to switch to while the button is pressed</summary>
    [SerializeField] private Material m_materialPressed;

    private void SetPlateMaterial(Material material)
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

    private void Press()
    {
        SetPlateMaterial(m_materialPressed);   //Switch to the pressed material
        m_plate.transform.position += new Vector3(0.0f, m_depressionDistance, 0.0f);   //Depress the button by depression distance
    }

    private void Release()
    {
        SetPlateMaterial(m_materialUnpressed);    //Switch to the unpressed material
        m_plate.transform.position += new Vector3(0.0f, -m_depressionDistance, 0.0f);   //press the button by negative depression distance
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (!m_triggered) //Check the button isn't already pressed
        {
            if (other.tag == "Player")  //If the player is the one colliding with it, press it
            {
                Trigger();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (m_triggered)
        {
            if (other.tag == "Player")  //If the player is the one colliding with it, press it
            {
                Detrigger();
            }
        }
    }
}
