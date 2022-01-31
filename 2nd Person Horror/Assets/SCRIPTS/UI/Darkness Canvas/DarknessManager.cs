using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarknessManager : MonoBehaviour
{

    DarkCanvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<DarkCanvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
