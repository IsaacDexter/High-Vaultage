using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_enemyBullet : MonoBehaviour
{
	[SerializeField] GameObject m_explosionEffect;
	public float m_bulletforce;
	// Start is called before the first frame update
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.transform.root.gameObject.tag == "player")
		{ 
			Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
			rigidbody.AddExplosionForce(m_bulletforce, transform.position, 100, 0f, ForceMode.Impulse);
		}
		GameObject Explosion = Instantiate(m_explosionEffect, gameObject.transform.position, transform.rotation);
		Destroy(Explosion, 5);
		Destroy(gameObject, 0.1f);
	}

}
