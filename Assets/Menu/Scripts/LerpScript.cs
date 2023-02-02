using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScript : MonoBehaviour
{

    private Vector3 endPosition;
    private Vector3 startPosition;
    private float desiredDuration;
    private float elapsedTime;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0,1,percentageComplete));
      // Debug.Log(transform.position);
    }

    public IEnumerator LevelSelectLerp(Canvas UILevel)
    {
        // First Lerp
        elapsedTime = 0;
        desiredDuration = (elapsedTime + 3f);
        startPosition = transform.position;
        endPosition = new Vector3(547.12f, 167.17f, -390.43f);


        yield return new WaitForSeconds(3f);

        if (transform.position != endPosition)
        {
            yield return new WaitForSeconds(.5f);
        }

        // Second Lerp
        elapsedTime = 0;
        desiredDuration = (elapsedTime + 3f);
        startPosition = transform.position;
        endPosition = new Vector3(550.35f, 167.17f, -390.43f);

        yield return new WaitForSeconds(3f);

        if (transform.position != endPosition)
        {
            yield return new WaitForSeconds(.5f);
        }


        // Third Lerp
        elapsedTime = 0;
        desiredDuration = (elapsedTime + 3f);
        startPosition = transform.position;
        endPosition = new Vector3(550.35f, 166.83f, -389.54f);

        yield return new WaitForSeconds(3f);

        if (transform.position != endPosition)
        {
            yield return new WaitForSeconds(.5f);
        }

        UILevel.enabled = true;
    }


    public IEnumerator LevelSelectedLerp(GameObject Player)
    {
        // First Lerp
        elapsedTime = 0;
        desiredDuration = (elapsedTime + 3f);
        startPosition = transform.position;
        endPosition = new Vector3(550.35f, 167.17f, -390.43f);


        yield return new WaitForSeconds(3f);

        if (transform.position != endPosition)
        {
            yield return new WaitForSeconds(.5f);
        }


        Player.SetActive(true);
        gameObject.SetActive(false);
    }
}
