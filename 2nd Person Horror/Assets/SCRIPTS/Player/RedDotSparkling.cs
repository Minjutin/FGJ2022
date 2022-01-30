using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDotSparkling : MonoBehaviour
{
    public GameObject redDot;

    //If you want to change times edit these.
    public float visibleTime = 0.5f;
    public float invisibleTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Sparkling");
    }

    private IEnumerator Sparkling()
    {
        while (true)
        {            
            redDot.SetActive(true);
            yield return new WaitForSeconds(visibleTime);
            redDot.SetActive(false);
            yield return new WaitForSeconds(invisibleTime);

        }
    }

}
