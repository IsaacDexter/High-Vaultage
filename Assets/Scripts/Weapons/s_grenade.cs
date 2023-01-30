using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_grenade : MonoBehaviour
{
    Rigidbody m_rigidbody;
    bool m_hasJoint;
    
    bool m_isActivated;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        var contact : contactPoint = other.c
        if (other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>() != null && !m_hasJoint && other.gameObject.tag != "Player" && !m_isActivated)
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
            m_hasJoint = true;

            //m_rigidbody.isKinematic = true;

            m_isActivated = true;
            //change colour

        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && !m_hasJoint && other.gameObject.tag != "Player" && !m_isActivated)
        {
            m_rigidbody.isKinematic = true;
            m_isActivated = true;

            //change colour
        }

    }



    public void Detonate(float force, float radius, float dammage)
    {
        RaycastHit[] hit;
        List<GameObject> targets = new List<GameObject>();


        hit = Physics.SphereCastAll(transform.position, radius, transform.forward);

        for (int i = 0; i < hit.Length; i++)
        {
            GameObject target = hit[i].transform.root.gameObject;
            if (!targets.Contains(target))
            {  
                targets.Add(target); 
            }
        }

        for (int i = 0; i < targets.Count; i++)
        { 

            Rigidbody rigidbody = targets[i].GetComponent<Rigidbody>();
            float distance = Vector3.Distance(targets[i].transform.position, transform.position);

            if (targets[i].tag == "Player")
            {
                if (distance <= radius)
                {
                    float multiplier = 1 - distance / radius;
                    rigidbody.AddExplosionForce(force, transform.position, radius, 0f, ForceMode.Impulse);
                }
            }
            if (targets[i].tag == "Enemy")
            {
                s_enemyHealth health = targets[i].GetComponent<s_enemyHealth>();
                health.DamageEnemy(dammage);
                float multiplier = 1 - distance / radius;
                rigidbody.AddExplosionForce(force, transform.position, radius, 0f, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }

}

