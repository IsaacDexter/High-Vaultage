using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    //Variables
    [SerializeField] e_weaponType m_primaryWeapon;
    [SerializeField] e_weaponType m_secondaryWeapon;
    [SerializeField] GameObject m_camera;

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
    }

    IEnumerator DashDelay(float delay)
	{
        yield return new WaitForSeconds(delay);
        m_rigidBody.velocity = startingVelcity;
    }

    // Update is called once per frame
    void Update()
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



        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            //open weapon wheel
            Debug.Log("Wheapon Wheel");
            if (Cursor.visible == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
			{
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
			}
        }


    }

    public void SwitchWeapon(e_weaponType weaponType,float arm)
	{
        if (arm == 1)
		{
            m_primaryWeapon = weaponType;
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

