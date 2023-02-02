using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_doorOpenerTrigger : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Transform door;
    [SerializeField] Animator animator;
    [SerializeField] string CheckTag = "Player";

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CheckTag))
        {
            animator.SetBool("approach", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CheckTag))
        {
            animator.SetBool("approach", false);
        }
    }
}

