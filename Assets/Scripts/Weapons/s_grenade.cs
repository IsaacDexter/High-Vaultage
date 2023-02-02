using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class s_grenade : MonoBehaviour
{
    Rigidbody m_rigidbody;
    bool m_hasJoint;
    ConstraintSource constraintSource;
    bool m_isActivated;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }


<<<<<<< HEAD

=======
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>() != null && !m_hasJoint && other.gameObject.tag != "Player" && !m_isActivated)
    //    {
    //        gameObject.AddComponent<FixedJoint>();
    //        gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
    //        m_hasJoint = true;

    //        //m_rigidbody.isKinematic = true;

    //        m_isActivated = true;
    //        //change colour

    //    }
    //    else if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && !m_hasJoint && other.gameObject.tag != "Player" && !m_isActivated)
    //    {
    //        m_rigidbody.isKinematic = true;
    //        m_isActivated = true;

    //        //change colour
    //    }

    //}
>>>>>>> parent of a5715fb... Finishing off melee weapons

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.transform.root.gameObject.tag!="Player")
        {
            Debug.Log("Poon");
            Vector3 colisionPoint = collision.contacts[0].point;
            
            ParentConstraint constraint = GetComponent<ParentConstraint>();
            constraintSource.sourceTransform = collision.gameObject.transform;
            //constraintSource.weight = 100;

            constraint.AddSource(constraintSource);
            
            //gameObject.transform.SetParent(collision.gameObject.transform,true);
            gameObject.transform.position = colisionPoint;
            m_rigidbody.isKinematic = true;
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

