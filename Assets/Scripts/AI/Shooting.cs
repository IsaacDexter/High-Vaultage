using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private float m_firingForce = 20f;
    [SerializeField] private float shootTimer;


    private void Start()
    {
        ResetShootTimer();
    }


    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            GameObject tempRef = Instantiate<GameObject>(projectile, transform.position + (transform.forward * 1.5f), Quaternion.identity); //This Spawns the bullet
            Vector3 direction = transform.forward;
            tempRef.GetComponent<Rigidbody>().AddForce(direction * m_firingForce, ForceMode.Impulse);

            ResetShootTimer();
        }
    }


    void ResetShootTimer()
    {
        shootTimer = 3f;
    }
}
