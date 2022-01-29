using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Charger_Room : MonoBehaviour
{
    //whenever you arrive in charger room this will be true.
    public bool canCharge = false;

    //whenever you leave your gun in the charge station this will be true.
    public bool thereIsGun = false;

    //gun can be obtained.
    public bool gunIsReady = false;

    //timer variables.
    public int maxTime = 5;
    public int timer;

    //getting stuff from Gun-script.
    Gun gun;
    bool gunLoaded;

    void Awake()
    {
        gun = gameObject.GetComponent<Gun>();

    }

    void Update()
    {
        if (canCharge && !thereIsGun && Input.GetKeyDown("space"))
        {
            gunLoaded = gameObject.GetComponent<Gun>().canBeUsed;
            if (!gunLoaded)
            {
                thereIsGun = true;            
                StartCoroutine("Countdown");
            }
        }

        //if everything is fine make gun usable.
        if (gunIsReady && canCharge && Input.GetKeyDown("space"))
        {
            gameObject.GetComponent<Gun>().canBeUsed = true;
            thereIsGun = false;
            gunIsReady = false;
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

    //countdown.
    private IEnumerator Countdown()
    {
        timer = maxTime;
        while (timer > 0)
        {

            timer -= 1;
            yield return new WaitForSeconds(1);
        }
        timer = maxTime;
        gunIsReady = true;
        //after countdown.
    }

}
