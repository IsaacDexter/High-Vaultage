using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_meleeBox : MonoBehaviour
{
	// Start is called before the first frame update
	public List<GameObject> m_targets = new List<GameObject>();
	public s_hand m_hand;

	private void OnTriggerEnter(Collider other)
	{
		if (!m_targets.Contains(other.transform.root.gameObject))
		{
			m_targets.Add(other.transform.root.gameObject);
		}


		if(m_hand.m_killOnHit == true)
		{
			if (other.transform.root.gameObject.tag == "Enemy")
			{
				other.transform.root.gameObject.GetComponent<s_enemyHealth>().DamageEnemy(15.0f); ;
				m_hand.m_charge += 0.25f;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (m_targets.Contains(other.transform.root.gameObject))
		{
			m_targets.Remove(other.transform.root.gameObject);
		}
	}
}
