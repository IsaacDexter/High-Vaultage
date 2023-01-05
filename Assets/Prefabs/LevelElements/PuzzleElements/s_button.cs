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
    /// <summary>A reference to the plates material</summary>
    Material m_plateMaterial;

    /// <summary>The material to use while the button is not pressed</summary>
    [SerializeField] private Material m_materialUnpressed;
    /// <summary>The material to switch to while the button is pressed</summary>
    [SerializeField] private Material m_materialPressed;

    // Start is called before the first frame update
    void Start()
    {
        m_plateMaterial = m_plate.GetComponent<MeshRenderer>().material; //Gets the material component
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Press()
    {
        m_pressed = true;
        SetPlateMaterial(m_materialPressed);   //Switch to the pressed material
        m_plate.transform.position += new Vector3(0.0f, m_depressionDistance, 0.0f);   //Depress the button by depression distance
    }

    void SetPlateMaterial(Material material)
    {
        m_plate.GetComponent<MeshRenderer>().material = material;
    }

    void Release()
    {
        m_pressed = false;
        SetPlateMaterial(m_materialUnpressed);    //Switch to the unpressed material
        m_plate.transform.position += new Vector3(0.0f, -m_depressionDistance, 0.0f);   //press the button by negative depression distance
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
