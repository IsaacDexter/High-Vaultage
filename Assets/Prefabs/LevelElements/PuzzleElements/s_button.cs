using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_button : MonoBehaviour
{
    /// <summary>If the button is pressed or not</summary>
    bool m_pressed = false;
    /// <summary>How far to depress the button when its pressed</summary>
    float m_depressionDistance = -0.1f;

    /// <summary>A reference to the buttons plate</summary>
    [SerializeField] private GameObject m_plate;

    /// <summary>The material to use while the button is not pressed</summary>
    [SerializeField] private Material m_materialUnpressed;
    /// <summary>The material to switch to while the button is pressed</summary>
    [SerializeField] private Material m_materialPressed;

    /// <summary>The game objects to activate when this button is pushed</summary>
    [SerializeField] private s_output[] m_outputs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Press()
    {
        m_pressed = true;
        SetPlateMaterial(m_materialPressed);   //Switch to the pressed material
        m_plate.transform.position += new Vector3(0.0f, m_depressionDistance, 0.0f);   //Depress the button by depression distance

        foreach (s_output output in m_outputs)
        {
            output.Activate();
        }
    }

    private void SetPlateMaterial(Material material)
    {
        m_plate.GetComponent<MeshRenderer>().material = material;
    }

    private void Release()
    {
        m_pressed = false;
        SetPlateMaterial(m_materialUnpressed);    //Switch to the unpressed material
        m_plate.transform.position += new Vector3(0.0f, -m_depressionDistance, 0.0f);   //press the button by negative depression distance

        foreach (s_output output in m_outputs)
        {
            output.Deactivate();
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (!m_pressed) //Check the button isn't already pressed
        {
            if (other.tag == "Player")  //If the player is the one colliding with it, press it
            {
                Press();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (m_pressed)
        {
            Release();
        }
    }
}
