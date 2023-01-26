using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_bullet : MonoBehaviour
{
    s_enemyHealth m_enemy;
    public float m_damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.gameObject.transform.root);
		if (other.gameObject.transform.root.tag == "Enemy")
		{
			Debug.Log("hit");
			m_enemy = other.gameObject.transform.root.GetComponent<s_enemyHealth>();
			m_enemy.DamageEnemy(m_damage);
		}
	}



	//private void OnCollisionEnter(Collision collision)
	//{
	//	if (collision.gameObject.tag == "Enemy")
	//	{
	//		Debug.Log("hit");
	//		m_enemy = collision.gameObject.GetComponent<s_enemyHealth>();
	//		m_enemy.DamageEnemy(m_damage);
	//	}
	//}
}
