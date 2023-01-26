//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TriggerEntered : MonoBehaviour
//{
//    [Header("Data")]
//    [SerializeField] int id;
//    [SerializeField] string LookForColliderName = "Player";

//    private void OnTriggerEnter(Collider other)
//    {
//        // only trigger if 'Player' collides
//        if (other.CompareTag(LookForColliderName))
//        {
//            TriggerEventManager.current.TriggerEvent(id);
//        }
//    }
//}
