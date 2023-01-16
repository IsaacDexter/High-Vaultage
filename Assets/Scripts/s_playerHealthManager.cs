using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_playerHealthManager : MonoBehaviour
{
    #region Initialisation

    private Rigidbody m_rigidbody;
    private Transform m_transform;
    private Camera m_camera;
    private bool m_damaged = false;
    private float m_recoveryTime;
    [SerializeField] private float m_recoveryDuration = 3.0f;
    [SerializeField] private CollisionDetectionMode m_collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    public s_spawnPoint m_spawnPoint;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = m_rigidbody.transform;
        m_camera = GetComponent<Camera>();
        //Sets the rigidbody's collision detection mode, used to help in collision detection with fast moving object
        m_rigidbody.collisionDetectionMode = m_collisionDetectionMode;
    }

    #endregion

    #region Damaging

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EnemyBullet"))
        {
            Destroy(collision.collider.gameObject);
            Damage();
        }
    }

    public void Damage(bool kill = false)
    {
        if (m_damaged || kill)
        {
            Kill();
        }
        else
        {
            m_damaged = true;
            m_recoveryTime = Time.time + m_recoveryDuration;
        }
    }
    public void Kill()
    {
        Respawn();
    }

    public void Respawn()
    {
        Transform spawnTransform = m_spawnPoint.transform;
        m_transform.position = spawnTransform.position;
        GetComponent<PlayerLook>().SetRotation(spawnTransform.rotation);

        //string scene = m_spawnPoint.m_scene;

        //GetComponent<s_levelLoader>().UnloadLevel(scene);
        //GetComponent<s_levelLoader>().LoadLevel(scene);
        GetComponent<s_levelLoader>().ReloadLevel();
    }

    #endregion

    #region Recovering

    private void Update()
    {
        if(m_damaged)
        {
            Regenerate();
        }
    }

    private void Regenerate()
    {
        if (Time.time >= m_recoveryTime)
        {
            Recover();
        }
    }

    public void Recover()
    {
        m_damaged = false;
    }

    #endregion
}
