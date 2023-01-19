using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    public Transform door;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, door.position);

        if (distance <= 15)
        {
            anim.SetBool("approach", true);
        }
        else
        {
            anim.SetBool("approach", false);

        }
    }
}
