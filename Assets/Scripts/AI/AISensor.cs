using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField] float m_distance = 10;
    [SerializeField] float m_angle = 30;
    [SerializeField] float m_height = 1.0f;
    [SerializeField] Color m_meshColor = Color.red;
    [SerializeField] int m_scanFrequency;
    [SerializeField] LayerMask m_layers;
    [SerializeField] LayerMask m_occlusionLayers;
    [SerializeField] float m_rotateSpeed;
    [SerializeField] List<GameObject> m_objects = new List<GameObject>();
    [SerializeField] Transform m_gunpoint;
    [SerializeField] GameObject TurretBody;

    bool m_shooting;
    Vector3 m_targetDirection;
    Collider[] m_colliders = new Collider[50];
    Mesh m_mesh;
    int m_count;
    GameObject m_detectedPlayer;

    bool m_resetCheck;
    bool m_reseting;
    [SerializeField] GameObject m_enemyShot;
    [SerializeField] float m_fireSpeed;
    [SerializeField] float m_currentReload;
    [SerializeField] float m_reloadTime;
    [SerializeField] float m_resetTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScanDelay(m_scanFrequency));
    }

    IEnumerator ScanDelay(float delay)
    {
        Scan();
        //Debug.Log("scan");
        yield return new WaitForSeconds(delay);
        StartCoroutine(ScanDelay(m_scanFrequency));
    }

    IEnumerator ResetAngle()
    {
        
        yield return new WaitForSeconds(m_resetTime);
        if (m_resetCheck == true)
        {
            m_reseting= true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(m_detectedPlayer!=null)
		{
            if (m_resetCheck == true)
            {
                m_resetCheck = false;
                m_reseting = false;
            }
            Vector3 dir = m_detectedPlayer.gameObject.transform.root.gameObject.transform.position - transform.position; //a vector pointing from pointA to pointB
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up); //calc a rotation that
            Quaternion rotation = Quaternion.Lerp(TurretBody.transform.rotation, rot, m_rotateSpeed);//Quaternion.Euler(new Vector3(0, rot.y,0))
            TurretBody.transform.rotation = rotation;

            Quaternion bRot = Quaternion.LookRotation(dir, Vector3.up);

            if(m_shooting)
			{
                m_currentReload += Time.deltaTime;
                if(m_currentReload > m_reloadTime)
				{
                    Shoot();
                    m_currentReload = 0;
                }
            }
            else
			{
                m_currentReload = 0;
            }


        }
        else
		{
            if (m_resetCheck == false)
            {
                m_resetCheck = true;
                StartCoroutine(ResetAngle());
            }
            if(m_reseting)
			{
                Quaternion rot = Quaternion.Euler( new Vector3(transform.rotation.x, 0, transform.rotation.z));
                Quaternion rotation = Quaternion.Lerp(TurretBody.transform.rotation, rot, m_rotateSpeed/4);
                TurretBody.transform.rotation = rotation;
                if(TurretBody.transform.rotation==rot)
				{
                    m_reseting = false;
                    m_resetCheck = false;
                }
            }
            StartCoroutine(ResetAngle());
        }

  //      for(int i = 0; i < m_firePoint.Length; i++)
		//{

		//}

        //m_scanTimer = Time.deltaTime;
        //if (m_scanTimer > 0)
        //{
        //    m_scanTimer += m_scanInterval;
        //    Scan();
        //    Quaternion toRotation = Quaternion.FromToRotation(transform.forward, m_targetDirection);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, m_rotateSpeed * Time.time);
        //}
        

    }

    private void Scan()
    {
        m_count = Physics.OverlapSphereNonAlloc(transform.position, m_distance, m_colliders, m_layers, QueryTriggerInteraction.Collide);

        m_objects.Clear();
        for (int i = 0; i < m_count; ++i)
        {
            GameObject obj = m_colliders[i].gameObject;
            if (IsInSight(obj))
            {
                m_detectedPlayer = obj;
                //Debug.Log("Player in range");
                if(!m_shooting)
				{
                    m_shooting = true;
                }
            }
			else
			{
                m_detectedPlayer = null;
                m_shooting = false;
            }
        }
        if(m_count<1)
		{
            m_detectedPlayer = null;
            m_shooting = false;
        }
    }

    void Shoot()
	{
        //Debug.Log("shoot");
        Vector3 direction = TurretBody.transform.TransformDirection(Vector3.forward);
        GameObject shot = Instantiate(m_enemyShot, m_gunpoint.position, TurretBody.transform.rotation);
        shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * m_fireSpeed);
        Destroy(shot, 2);

    }


    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        
        if (direction.y < 0 || direction.y > m_height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > m_angle)
        {
            return false;
        }

        origin.y += m_height / 2;
        dest.y = origin.y;

        if (Physics.Linecast(origin, dest, m_occlusionLayers))
        {
            return false;
        }
        return true;
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -m_angle, 0) * Vector3.forward * m_distance;
        Vector3 bottomRight = Quaternion.Euler(0, m_angle, 0) * Vector3.forward * m_distance;

        Vector3 topCenter = bottomCenter + Vector3.up * m_height;
        Vector3 topRight = bottomRight + Vector3.up * m_height;
        Vector3 topLeft = bottomLeft + Vector3.up * m_height;

        int vert = 0;

        // Left Side
        vertices[vert++] = bottomCenter; 
        vertices[vert++] = bottomLeft; 
        vertices[vert++] = topLeft;  

        vertices[vert++] = topLeft; 
        vertices[vert++] = topCenter;  
        vertices[vert++] = bottomCenter; 

        //Right Side
        vertices[vert++] = bottomCenter;  
        vertices[vert++] = topCenter; 
        vertices[vert++] = topRight; 

        vertices[vert++] = topRight; 
        vertices[vert++] = bottomRight; 
        vertices[vert++] = bottomCenter; 

        float currentAngle = -m_angle;
        float deltaAngle = (m_angle * 2) / segments;
        for(int i =0; i < segments; i++)
        {
            
             bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * m_distance;
             bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * m_distance;

             topRight = bottomRight + Vector3.up * m_height;
             topLeft = bottomLeft + Vector3.up * m_height;

            //Far Side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //Top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //Bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }
        
        for(int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        m_mesh = CreateWedgeMesh();
    }

    private void OnDrawGizmos()
    {
        if (m_mesh)
        {
            Gizmos.color = m_meshColor;
            Gizmos.DrawMesh(m_mesh, transform.position, transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, m_distance);
        for (int i = 0; i < m_count; ++i)
        {
            Gizmos.DrawSphere(m_colliders[i].transform.position, 0.2f);
        }

        Gizmos.color = Color.green;

        foreach (var obj in m_objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }
}
