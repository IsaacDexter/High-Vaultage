using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullets : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Countdown(2f));

        if (collision.gameObject)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Countdown(float life)
    {
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
}
