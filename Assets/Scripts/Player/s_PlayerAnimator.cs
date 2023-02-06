using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_PlayerAnimator : MonoBehaviour
{
    private Animator m_leftAnimator;
    private Animator m_rightAnimator;

    [Header("Arms")]
    [SerializeField] GameObject m_leftArm;
    [SerializeField] GameObject m_rightArm;

    void Start()
    {
        m_leftAnimator = m_leftArm.GetComponent<Animator>();
        m_rightAnimator = m_rightArm.GetComponent<Animator>();

    }

    void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        if (gameObject.GetComponent<s_player>().m_moveDirection == Vector3.zero)
        {
            m_leftAnimator.SetFloat("MovementSpeed", 0);
            m_rightAnimator.SetFloat("MovementSpeed", 0);
        }
        else if (gameObject.GetComponent<s_player>().m_moveDirection != Vector3.zero && gameObject.GetComponent<s_player>().m_grounded && !gameObject.GetComponent<s_player>().m_sliding)
        {
            m_leftAnimator.SetFloat("MovementSpeed", 1);
            m_rightAnimator.SetFloat("MovementSpeed", 1);
        }

        if (gameObject.GetComponent<s_player>().m_sliding)
        {
            m_leftAnimator.SetTrigger("StartSlide");
            m_rightAnimator.SetTrigger("StartSlide");

        }
        else if (!gameObject.GetComponent<s_player>().m_sliding)
        {
            m_leftAnimator.ResetTrigger("StartSlide");
            m_rightAnimator.ResetTrigger("StartSlide");
        }
    }
}
