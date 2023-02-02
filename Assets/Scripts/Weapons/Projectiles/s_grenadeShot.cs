using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_grenadeShot : s_stickyProjectile
{
    /// <summary>Once something has entered the trigger i.e. the grenade has hit an object</summary>
    /// <param name="other">The other thing that entered the trigger i.e. whatever the object attatched itself to</param>
    /// 

    [SerializeField] GameObject m_explosionEffect;

	private void OnCollisionEnter(Collision other)
    {
        if (!m_stuck)                               //If we havent yet attatched ourselves to something...
        {
            if (other.gameObject.tag != "Player")   //And whatever we've hit isnt a player
            {
                m_stuck = Stick(other);  //Stick to whatever we hit
            }
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
            }
        }
        GameObject Explosion =  Instantiate(m_explosionEffect, gameObject.transform.position, transform.rotation);
        Destroy(Explosion, 5);

        Destroy(gameObject);

        

    }
}
