using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_playerHealthManager : MonoBehaviour
{
    #region Initialisation

    /// <summary>The physics/collision component of the player, used to determine hit events</summary>
    private Rigidbody m_rigidbody;
    /// <summary>The transform (position and rotation) of that rigidbody</summary>
    private Transform m_transform;
    /// <summary>The camera, used when transforming the player when it respawns</summary>
    private Camera m_camera;
    /// <summary>The players current damaged state. If damaged, the player will die when it takes damage</summary>
    private bool m_damaged = false;
    /// <summary>When the player will be recovered</summary>
    private float m_recoveryTime;
    /// <summary>The delay in seconds between being damaged and no longer being damaged.</summary>
    [SerializeField] private float m_recoveryDuration = 3.0f;
    /// <summary>The collision detection mode of the player. Continuous dynamic/speculative are good for fast moving projectiles.</summary>
    [SerializeField] private CollisionDetectionMode m_collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    /// <summary>The player's spawn point it will return to upon death.</summary>
    public Transform m_spawnPoint;

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
        if (collision.collider.CompareTag("EnemyBullet"))   //If we have collided with a bullet...
        {
            Destroy(collision.collider.gameObject); //Destroy that bullet
            Damage();   //Take damage.
        }
    }

    public void Damage()
    {
        if (m_damaged)  //If we're already damaged, kill the playewr
        {
            Kill();
        }
        else    //Otherwise...
        {
            m_damaged = true;   //Set us to be damaged
            m_recoveryTime = Time.time + m_recoveryDuration;    //Set a time when we will be restored
        }
    }

    /// <summary>Called when the player takes damaged while damaged, or can be called publicly for instakills. Will call respawn.</summary>
    public void Kill()
    {
        Respawn();
    }

    /// <summary>Transform the player to the spawnpoints transform, and then reload the current level</summary>
    public void Respawn()
    { 
        m_transform.position = m_spawnPoint.position;   //Move the player to the position of the spawnpoint
        GetComponent<PlayerLook>().SetRotation(m_spawnPoint.rotation);  //Turn the player to face the same direction as the spawnpoint. Currently nonfunctional due to issues with turning code

        GetComponent<s_levelLoader>().ReloadLevel();    //Call the level loader to reload the current level. Will not reload the corridor though, luckily.
    }

    #endregion

    #region Recovering

    private void Update()
    {
        if(m_damaged)   //If we're damaged,
        {
            Regenerate();   //Start healing that damage
        }
    }

    /// <summary>Checks if we've exceeded recovery time, if we have, recover.</summary>
    private void Regenerate()
    {
        if (Time.time >= m_recoveryTime)    //If we've reached the time we knew we'd've recovered by
        {
            Recover();  //Recover
        }
    }

    /// <summary>Set m_damaged to false. Include any visual effects later if you like. IDC</summary>
    public void Recover()
    {
        m_damaged = false;
    }

    #endregion
}
