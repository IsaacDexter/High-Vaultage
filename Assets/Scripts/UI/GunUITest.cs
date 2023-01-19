using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GunUITest : MonoBehaviour
{
	[SerializeField] GameObject m_player;
    private GunManager m_gunManager;


    // Start is called before the first frame update
    void Start()
    {
        m_gunManager = m_player.GetComponent<GunManager>();
    }

     public void SwitchWeapon(e_weaponType weaponType, float arm)
     {
         m_gunManager.SwitchWeapon(weaponType, arm);
     }

}
