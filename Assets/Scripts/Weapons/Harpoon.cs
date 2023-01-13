using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    Rigidbody m_rigidbody;
    bool m_hasJoint;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() != null && !m_hasJoint && collision.gameObject.tag!="Player")
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            m_hasJoint = true;
            m_rigidbody.isKinematic = true;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !m_hasJoint&& collision.gameObject.tag != "Player")
        {
            m_rigidbody.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
