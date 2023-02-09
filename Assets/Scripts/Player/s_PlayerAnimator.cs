using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_PlayerAnimator : MonoBehaviour
{
    // Animators
    private Animator m_leftAnimator;
    private Animator m_rightAnimator;

    // The player script
    private s_player player;

    // Hand scripts
    private s_hand m_leftHand;
    private s_hand m_rightHand;

    [Header("Arms")]
    [SerializeField] GameObject m_leftArm;
    [SerializeField] GameObject m_rightArm;
    [SerializeField] GameObject m_leftHandEmpty;
    [SerializeField] GameObject m_rightHandEmpty;

    void Start()
    {
        m_leftAnimator = m_leftArm.GetComponent<Animator>();
        m_rightAnimator = m_rightArm.GetComponent<Animator>();

        player = GetComponent<s_player>();
        m_leftHand = m_leftHandEmpty.GetComponent<s_hand>();
        m_rightHand = m_rightHandEmpty.GetComponent<s_hand>();
    }

    void Update()
    {
        HandleAnimations();
        HandleWeaponAnimations();
    }

    private void HandleAnimations()
    {
        if (player.m_moveDirection == Vector3.zero)
        {
            m_leftAnimator.SetFloat("MovementSpeed", 0);
            m_rightAnimator.SetFloat("MovementSpeed", 0);
        }
        else if (player.m_moveDirection != Vector3.zero && player.m_grounded && !player.m_sliding)
        {
            m_leftAnimator.SetFloat("MovementSpeed", 1);
            m_rightAnimator.SetFloat("MovementSpeed", 1);
        }

        if (player.m_sliding)
        {
            m_leftAnimator.SetTrigger("StartSlide");
            m_rightAnimator.SetTrigger("StartSlide");

        }
        else if (!player.m_sliding)
        {
            m_leftAnimator.ResetTrigger("StartSlide");
            m_rightAnimator.ResetTrigger("StartSlide");
        }
    }

    private void HandleWeaponAnimations()
    {
        switch (m_leftHand.m_weapon.name)
        {
            case "aaa":

                break;

            default:
                Debug.Log(m_leftHand.m_weapon.name);
                break;
        }
    }
}
