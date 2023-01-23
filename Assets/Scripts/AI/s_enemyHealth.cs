using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_enemyHealth : MonoBehaviour
{
    [SerializeField]
    private float enemyHealth;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamageEnemy(float damage)
	{
        enemyHealth -= damage;
	}


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        enemyHealth = enemyHealth - 1;

    //        Debug.Log("Hit Enemy");
    //    }

    //}

}

