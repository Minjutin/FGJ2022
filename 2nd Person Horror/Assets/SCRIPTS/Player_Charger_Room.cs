using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Charger_Room : MonoBehaviour
{

    bool canCharge = false;

    //getting stuff from Gun-script.
    //Gun gunLoaded = GetComponent<Gun>();
    //bool isLoaded = gunLoaded.isLoaded;

    Gun gun;
    bool gunLoaded;

    void Awake()
    {
        gun = gameObject.GetComponent<Gun>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canCharge && Input.GetKeyDown("space"))
        {
            gunLoaded = gameObject.GetComponent<Gun>().isLoaded;
            if (!gunLoaded)
            {
                Debug.Log("Gun is charging");
                gameObject.GetComponent<Gun>().isLoaded = true;
            }
            else
            {
                Debug.Log("Gun is already charged!");
            }
        }
    }

    //When player arrives a room where they can charge their gun.
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Charger")
        {
            canCharge = true;
        }
    }

    //When player exits a room where they can charge their gun.
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Charger")
        {
            canCharge = false;
        }
    }

}
