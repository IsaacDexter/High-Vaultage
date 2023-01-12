using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody m_rigidbody;
    bool m_hasJoint;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        
    }


	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.GetComponent<Rigidbody>() != null && !m_hasJoint && other.gameObject.tag != "Player")
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
            m_hasJoint = true;
            m_rigidbody.isKinematic = true;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Ground") && !m_hasJoint && other.gameObject.tag != "Player")
		{
            m_rigidbody.isKinematic = true;
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
