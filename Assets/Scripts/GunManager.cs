using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    //Variables
    [SerializeField] e_weaponType m_primaryWeapon;
    [SerializeField] e_weaponType m_secondaryWeapon;
    [SerializeField] GameObject m_camera;
    Rigidbody m_rigidBody;

    float m_primaryCharge;
    float m_secondaryCharge;

    float m_holdTimer;
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_primaryWeapon)
        {
            case e_weaponType.GLOVE:
                GloveAttack(KeyCode.Mouse0);
                break;
            case e_weaponType.SWORD:
                SwordAttack(KeyCode.Mouse0);
                break;
            case e_weaponType.SHOTGUN:
                ShotgunAttack(KeyCode.Mouse0);
                break;
            case e_weaponType.GRENADE:
                GrenadeAttack(KeyCode.Mouse0);
                break;
            case e_weaponType.HARPOON:
                HarpoonAttack(KeyCode.Mouse0);
                break;
            case e_weaponType.SHEILD:
                SheildAttack(KeyCode.Mouse0);
                break;
        }
        switch (m_secondaryWeapon)
        {
            case e_weaponType.GLOVE:
                GloveAttack(KeyCode.Mouse1);
                break;
            case e_weaponType.SWORD:
                SwordAttack(KeyCode.Mouse1);
                break;
            case e_weaponType.SHOTGUN:
                ShotgunAttack(KeyCode.Mouse1);
                break;
            case e_weaponType.GRENADE:
                GrenadeAttack(KeyCode.Mouse1);
                break;
            case e_weaponType.HARPOON:
                HarpoonAttack(KeyCode.Mouse1);
                break;
            case e_weaponType.SHEILD:
                SheildAttack(KeyCode.Mouse1);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            //open weapon wheel
            Debug.Log("Wheapon Wheel");
        }
    }

    void GloveAttack(KeyCode mouse)
	{
        if (Input.GetKeyDown(mouse))
        {
            m_holdTimer=Time.time;
            
        }
        if (Input.GetKeyUp(mouse))
        {
            float heldTime = Time.time - m_holdTimer;
            Debug.Log(heldTime);
            if (heldTime > 0.25)
			{
                float velocityCancel = m_rigidBody.velocity.y;
                Debug.Log("jump");
                Vector3 jumpforce = new Vector3(0, 10, 0);
                if(velocityCancel < 0)
				{
                    velocityCancel = 0;
                }
                m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, velocityCancel, m_rigidBody.velocity.z);
                m_rigidBody.AddForce(jumpforce,ForceMode.Impulse);
            }
            else
			{
                Debug.Log("punch");

            }
        }


    }


    void SwordAttack(KeyCode mouse)
    {
        Debug.Log("Sword Attack");
    }
    void ShotgunAttack(KeyCode mouse)
    {
        if (Input.GetKeyUp(mouse))
        {
            Debug.Log("Shogun");
            Vector3 forwards = m_camera.transform.forward;
            m_rigidBody.AddForce(forwards * -50,ForceMode.Impulse);
            Debug.Log(forwards * -50);
        }

    }
    void GrenadeAttack(KeyCode mouse)
    {
        Debug.Log("Grenade Attack" );
    }
    void HarpoonAttack(KeyCode mouse)
    {
        Debug.Log("Harpoon Attack");
    }
    void SheildAttack(KeyCode mouse)
    {
        Debug.Log("Sheild Attack2");
    }




}
public enum e_weaponType
{
    GLOVE,
    SWORD,
    SHOTGUN,
    GRENADE,
    HARPOON,
    SHEILD
}

