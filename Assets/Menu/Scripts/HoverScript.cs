using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverScript : MonoBehaviour
{

    float r;
    float g;
    float b;


    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<TMPro.TextMeshProUGUI>().color.r;
        g = gameObject.GetComponent<TMPro.TextMeshProUGUI>().color.g;
        b = gameObject.GetComponent<TMPro.TextMeshProUGUI>().color.b;
    }

   public void HoverEnter()
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255,235,0);
    }

    public void HoverExit()
    {
       gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(r,g,b);
    }

}
