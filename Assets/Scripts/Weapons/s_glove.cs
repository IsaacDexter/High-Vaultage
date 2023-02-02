using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_glove : s_chargingWeapon
{


    RaycastHit[] hit;
    List<GameObject> targets = new List<GameObject>();

    [Header("Uppercut Settings")]
    /// <summary>The force to launch the player upwards by, multiplied by the time spent charging.</summary>
    [SerializeField] private float m_uppercutForce;
    /// <summary>The minimum force to be launched upwards by</summary>
    [SerializeField] private float m_minForce;
    /// <summary>The maximum force to be launched forwards by</summary>
    [SerializeField] private float m_maxForce;
    /// <summary>The amount of charge required to uppercut instead of punch</summary>
    [SerializeField] private float m_uppercutCharge;
    [Header("Punch Settings")]
    /// <summary>How much it costs to perfom an punch as opposed to a big uppercut</summary>
    [SerializeField] private float m_punchCost;
    
    [SerializeField] private float m_fistDamage;

    public List<GameObject> m_meleeTargets;

    override public void Press()
    {
        m_hand.m_meleeBox.SetActive(true);

        m_startTime = Time.time;    //Store the time the weapon started charging

        m_charging = true;          //Start the weapon charging
        m_hand.m_regening = false;  //Prevent the hand from regenning ammo while charging

    }


    /// <summary>Sends the player upward according to cameras upwards vector with force proportional to the time spent charging</summary>
    override protected void Fire()
    {

        if (m_chargeTime>m_uppercutCharge)   //If we've accumulated enough charge to perform an uppercut...

		{
            if(CheckCost())                 //...And can afford it...
            {
                Uppercut();                 //...Uppercut.
            }
        }
        else                        //If we've only accumulated enough charge to perform a punch...
		{
            if (CheckPunchCost())   //...And can afford it...
            {
                Punch();            //...Punch.
            }
		}
    }

    private void Uppercut()
    {
        Vector3 direction = m_rigidBody.transform.up;   //Get the player's upwards direction
        m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, Mathf.Max(m_rigidBody.velocity.y, 0), m_rigidBody.velocity.z);   //Set the y to be at least 0
        float force = Mathf.Clamp(m_uppercutForce * m_chargeTime, m_minForce, m_maxForce);   //Calculate how much force to send the player up by
        m_rigidBody.AddForce(direction * force, ForceMode.Impulse);   //Apply an upwards force to the player


        m_meleeTargets = m_hand.m_meleeBox.GetComponent<s_meleeBox>().m_targets;

        for (int i = 0; i < m_meleeTargets.Count; i++)
        {
            if (m_meleeTargets[i].tag == "Enemy")        //if the hit object has the tag enemy...
            {
                Debug.Log("hit " + m_meleeTargets[i]);
                m_meleeTargets[i].GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
                m_meleeTargets[i].GetComponent<s_enemyHealth>().DamageEnemy(m_fistDamage);          //...destroy the enemy

            }
        }

        m_hand.m_meleeBox.SetActive(false);
    }

    private void Punch()
    {
        Debug.Log("Punch!");
        m_meleeTargets = m_hand.m_meleeBox.GetComponent<s_meleeBox>().m_targets;

        for (int i = 0; i < m_meleeTargets.Count; i++)
        {
            if (m_meleeTargets[i].tag == "Enemy")        //if the hit object has the tag enemy...
            {
                Debug.Log("hit " + m_meleeTargets[i]);

                m_meleeTargets[i].GetComponent<s_enemyHealth>().DamageEnemy(m_fistDamage);          //...destroy the enemy

            }
        }
        m_meleeTargets = null;
        m_hand.m_meleeBox.SetActive(false);
    }

    private bool CheckPunchCost()
    {
        if (m_hand.m_charge >= m_punchCost)    //If we can afford to punch...
        {
            m_hand.m_charge -= m_punchCost;    //...Pay the cost.
            return true;
        }
        else
        {
            return false;
        }
    }
}
