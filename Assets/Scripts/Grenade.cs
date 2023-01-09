using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        
    }

	private void OnCollisionEnter(Collision other)
    {

    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
