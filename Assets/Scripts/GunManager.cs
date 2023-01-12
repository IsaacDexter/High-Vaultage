using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    //Variables
    [SerializeField] e_weaponType m_primaryWeapon;
    [SerializeField] e_weaponType m_secondaryWeapon;
    [SerializeField] GameObject m_camera;
    [SerializeField] Transform m_gunpoint;
    private int m_primaryCharge = 100;
    private int m_secondaryCharge = 100;
    private int m_primaryMaxCharge = 100;
    private int m_secondaryMaxCharge = 100;
    private bool m_cancel;

    bool m_weaponWheel;


    [SerializeField] float m_punchForce;
    [SerializeField] float m_shotgunForce;
    [SerializeField] float m_dashForce;
    [SerializeField] float m_dashDuration;
    Rigidbody m_rigidBody;
    
    //sword var
    float m_holdTimer;
    float m_dashTime;
    Vector3 startingVelcity;

    //shotgun var
    [SerializeField] GameObject m_shotgunShot;
    [SerializeField] float m_shotgunPellets;
    [SerializeField] float m_shotgunSpread;
    [SerializeField] float m_shotgunSpeed;

    //grenade var
    [SerializeField] GameObject m_grenadeShot;
    [SerializeField] float m_grenadeSpeed;
    [SerializeField] float m_grenadeForce;
    [SerializeField] float m_grenadeRadius;
    private GameObject m_currentGrenade;
    bool m_grenadeSwitch;

    //harpoon var
    [SerializeField] GameObject m_harpoonShot;
    [SerializeField] float m_harpoonSpeed;
    [SerializeField] float m_harpoonForce;
    private GameObject m_currentHarpoon;
    SpringJoint joint;

    //revolver var
    private LineRenderer m_lineRenderer;
    private RaycastHit m_hit;
    [SerializeField] int m_revolverRange;
    [SerializeField] GameObject m_revolverShot;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    IEnumerator DashDelay(float delay)
	{
        yield return new WaitForSeconds(delay);
        if (!m_cancel)
        {
            m_rigidBody.velocity = startingVelcity;
        }
        else
		{
            m_cancel = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            //open weapon wheel
            Debug.Log("Wheapon Wheel");

            if (m_weaponWheel == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                m_weaponWheel = false;
                Time.timeScale = 1f;

            }
            else
			{
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                m_weaponWheel = true;
                Time.timeScale = 0.5f;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
		{
            m_cancel = true;
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
        if (Input.GetKeyDown(mouse))
        {
            m_holdTimer = Time.time;
        }
       
        if (Input.GetKeyUp(mouse))
        {
            m_cancel = false;
            float heldTime = Time.time - m_holdTimer;
            if (heldTime > 0.25)
            {
                Vector3 startingVelcity = m_rigidBody.velocity;
                m_rigidBody.AddForce(m_camera.transform.TransformDirection(Vector3.forward) * m_dashForce, ForceMode.Impulse);
                StartCoroutine(DashDelay(m_dashDuration));
            }
            else
            {
                Debug.Log("Slash");
            }
        }
    }

    void ShotgunAttack(KeyCode mouse)
    {
        if (Input.GetKeyDown(mouse))
        {
            Debug.Log("Shogun");
            Vector3 direction = m_camera.transform.TransformDirection(Vector3.forward);
            //m_rigidBody.AddForce(new Vector3(100, 0, 0), ForceMode.Impulse);
			for (int i = 0; i < m_shotgunPellets; i++)
			{
                //Vector3 fireAngle = direction + new Vector3(Random.Range(-m_shotgunSpread, m_shotgunSpread), Random.Range(-m_shotgunSpread, m_shotgunSpread), Random.Range(-m_shotgunSpread, m_shotgunSpread));
                Quaternion angle = Random.rotation;
                GameObject shot = Instantiate(m_shotgunShot, m_gunpoint.position, m_camera.transform.rotation);
                shot.transform.rotation = Quaternion.RotateTowards(shot.transform.rotation, angle, m_shotgunSpread);
                shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward*m_shotgunSpeed);
                Destroy(shot, 1);

            }
            direction *=-1;
            m_rigidBody.AddForce(direction * m_shotgunForce, ForceMode.Impulse);
		}

    }
    void GrenadeAttack(KeyCode mouse)
    {
        if (Input.GetKeyDown(mouse))
        {
            
            if (!m_grenadeSwitch)
            {
                Debug.Log("Grenade Attack");
                m_currentGrenade = Instantiate(m_grenadeShot, m_gunpoint.position, m_camera.transform.rotation);
                m_currentGrenade.GetComponent<Rigidbody>().AddForce(m_currentGrenade.transform.forward * m_grenadeSpeed);
                m_grenadeSwitch = true;
            }
			else
			{
                Debug.Log("Grenade Explode");
                m_rigidBody.AddExplosionForce(m_grenadeForce, m_currentGrenade.transform.position, m_grenadeRadius,0f,ForceMode.Impulse);

                Destroy(m_currentGrenade);
                m_grenadeSwitch = false;
                m_currentGrenade = null;
            }
        }
    }
    void HarpoonAttack(KeyCode mouse)
    {
        if (Input.GetKeyDown(mouse))
        {
            Debug.Log("Harpoon Attack");
            AttachHarpoon();
            m_currentHarpoon = Instantiate(m_harpoonShot, m_gunpoint.position, m_camera.transform.rotation);
            m_currentHarpoon.GetComponent<Rigidbody>().AddForce(m_currentHarpoon.transform.forward * m_harpoonSpeed);
            Vector3 swingPoint = m_currentHarpoon.transform.position;
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(gameObject.transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

        }
        if (Input.GetKeyUp(mouse))
        {
            m_rigidBody.AddExplosionForce(-m_harpoonForce, m_currentHarpoon.transform.position, 9999, 2f, ForceMode.Impulse);
            Destroy(m_currentHarpoon);
            m_currentHarpoon = null;
            Destroy(joint);
        }
        joint.connectedAnchor = m_currentHarpoon.transform.position;
    }
    void SheildAttack(KeyCode mouse)
    {
        Debug.Log("Sheild Attack2");

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
        if (Input.GetKeyDown(mouse))
        {
            Debug.Log("Revolver Attack");
            Time.timeScale = 0.25f;
        }
        if (Input.GetKeyUp(mouse))
        {
            Time.timeScale = 1f;

            
            Debug.DrawRay(m_gunpoint.position, m_camera.transform.forward* m_revolverRange, Color.green, 20);
            GameObject shotLineObject = Instantiate(m_revolverShot, m_gunpoint.position, Quaternion.Euler( m_camera.transform.forward));
            LineRenderer shotline = shotLineObject.GetComponent<LineRenderer>();
            shotline.SetPosition(0, m_gunpoint.position);
            shotline.SetPosition(1, m_camera.transform.forward.normalized * m_revolverRange);
            Destroy(shotLineObject, 1);
            if (Physics.Raycast(m_gunpoint.position, m_camera.transform.forward, out m_hit, m_revolverRange))
			{
                if (m_hit.rigidbody != null)
                {
                    GameObject hitEnemy = m_hit.rigidbody.transform.gameObject;
                    Debug.Log("hit "+ hitEnemy);
                    if (hitEnemy.tag == "Enemy")
					{
                        // call destroy on enemy
                        Destroy(hitEnemy);
					}
                }
			}
        }
    }

    public void AttachHarpoon()
	{

	}


}
public enum e_weaponType
{
    GLOVE,
    SWORD,
    SHOTGUN,
    GRENADE,
    HARPOON,
    REVOLVER,
    SHEILD
}

