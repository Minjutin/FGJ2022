using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public bool isLoaded = false;
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        if (isLoaded && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Gun is used");
            Instantiate(projectile, transform.position, Quaternion.identity);
            isLoaded = false;
        }
    }
}
