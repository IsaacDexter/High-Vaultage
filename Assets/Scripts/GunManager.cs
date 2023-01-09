using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    //Variables
    [SerializeField] e_weaponType m_primaryWeapon;
    [SerializeField] e_weaponType m_secondaryWeapon;
    [SerializeField] GameObject m_camera;

<<<<<<< HEAD
=======
    bool m_weaponWheel;

>>>>>>> Chris
    [SerializeField] float m_punchForce;
    [SerializeField] float m_shotgunForce;
    [SerializeField] float m_dashForce;
    [SerializeField] float m_dashDuration;
    [SerializeField] float m_grenadeForce;
    Rigidbody m_rigidBody;

    float m_primaryCharge;
    float m_secondaryCharge;
    
    float m_holdTimer;
    float m_dashTime;
    Vector3 startingVelcity;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
<<<<<<< HEAD
=======
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
>>>>>>> Chris
    }

    IEnumerator DashDelay(float delay)
	{
        yield return new WaitForSeconds(delay);
        m_rigidBody.velocity = startingVelcity;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //Shoot Selected Primary Weapon
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
        //Shoot Selected Secondary Weapon
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

=======
        if (!m_weaponWheel)
        {
            //Shoot Selected Primary Weapon
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
                    case e_weaponType.REVOLVER:
                    RevolverAttack(KeyCode.Mouse0);
                    break;
            }
            //Shoot Selected Secondary Weapon
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
                case e_weaponType.REVOLVER:
                    RevolverAttack(KeyCode.Mouse1);
                    break;
            }
        }
>>>>>>> Chris


        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            //open weapon wheel
            Debug.Log("Wheapon Wheel");
<<<<<<< HEAD
            if (Cursor.visible == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
=======
            if (m_weaponWheel == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                m_weaponWheel = false;
                Time.timeScale = 1f;
>>>>>>> Chris
            }
            else
			{
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
<<<<<<< HEAD
			}
=======
                m_weaponWheel = true;
                Time.timeScale = 0.5f;
            }
>>>>>>> Chris
        }


    }

    public void SwitchWeapon(e_weaponType weaponType,float arm)
	{
        if (arm == 1)
		{
            m_primaryWeapon = weaponType;
<<<<<<< HEAD
=======

>>>>>>> Chris
        }
        if (arm == 2)
		{
            m_secondaryWeapon = weaponType;
        }
	}

    void SetWeaponModel(e_weaponType weaponType, float arm)
	{

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
                Vector3 jumpforce = new Vector3(0, m_punchForce, 0);
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
<<<<<<< HEAD

=======
>>>>>>> Chris
            }
        }


    }


    void SwordAttack(KeyCode mouse)
    {
        Debug.Log("Sword Attack");
        if (Input.GetKeyUp(mouse))
        {
            Vector3 startingVelcity = m_rigidBody.velocity;
            m_rigidBody.AddForce(m_camera.transform.TransformDirection(Vector3.forward) * m_dashForce, ForceMode.Impulse);
            StartCoroutine(DashDelay(m_dashDuration));
        }
    }

    void ShotgunAttack(KeyCode mouse)
    {
        if (Input.GetKeyUp(mouse))
        {
            Debug.Log("Shogun");
            Vector3 direction = m_camera.transform.TransformDirection(Vector3.forward);
            direction *= -1;
            //m_rigidBody.AddForce(new Vector3(100, 0, 0), ForceMode.Impulse);
            m_rigidBody.AddForce(direction * m_shotgunForce, ForceMode.Impulse);
            Debug.DrawRay(m_camera.transform.position, direction, Color.green, 14);
        }

    }
    void GrenadeAttack(KeyCode mouse)
    {
        Debug.Log("Grenade Attack" );
<<<<<<< HEAD
=======



>>>>>>> Chris
    }
    void HarpoonAttack(KeyCode mouse)
    {
        Debug.Log("Harpoon Attack");
<<<<<<< HEAD
=======

>>>>>>> Chris
    }
    void SheildAttack(KeyCode mouse)
    {
        Debug.Log("Sheild Attack2");
<<<<<<< HEAD
=======
        if (Input.GetKeyDown(mouse))
        {
            m_rigidBody.useGravity = false;
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0,m_rigidBody.velocity.z);
        }
        if (Input.GetKeyUp(mouse))
        {
            m_rigidBody.useGravity = true;
        }
    }

    void RevolverAttack(KeyCode mouse)
    {
        Debug.Log("Revolver Attack");
        if (Input.GetKeyDown(mouse))
        {
            Time.timeScale = 0.25f;
        }
        if (Input.GetKeyUp(mouse))
        {
            Time.timeScale = 1f;
        }
>>>>>>> Chris
    }




}
public enum e_weaponType
{
    GLOVE,
    SWORD,
    SHOTGUN,
    GRENADE,
    HARPOON,
<<<<<<< HEAD
=======
    REVOLVER,
>>>>>>> Chris
    SHEILD
}

