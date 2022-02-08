using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Charger_Room : MonoBehaviour
{
    //whenever you arrive in charger room this will be true.
    public bool canCharge = false;
    public GameObject electricity;

    [SerializeField] Vector3 audioPos;
    [SerializeField] AudioClip arriveRoom;

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

    // Alerts the Monster
    MonsterPathfinding monster;

    void Awake()
    {
        gun = gameObject.GetComponent<Gun>();
    }
    private void Start()    // Sorry Milja, I just don't trust Awake() :D
    {
        monster = FindObjectOfType<MonsterPathfinding>();
        audioPos = FindObjectOfType<AudioManager>().gameObject.transform.position;
        // Add the code below to appropriate spot to wake the monster
        // monster.EnterHuntingMode(1f);
    }

    void Update()
    {
        if (canCharge && !thereIsGun && Input.GetKeyDown("e"))
        {
            gunLoaded = gameObject.GetComponent<Gun>().canBeUsed;
            if (!gunLoaded)
            {
                thereIsGun = true;            
                StartCoroutine("Countdown");
            }
        }

        //if everything is fine make gun usable.
        if (gunIsReady && canCharge && Input.GetKeyDown("e"))
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
            electricity.SetActive(true);
            AudioSource.PlayClipAtPoint(arriveRoom, audioPos);
            canCharge = true;
        }
    }

    //When player exits a room where they can charge their gun.
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Charger")
        {
            electricity.SetActive(false);
            canCharge = false;
        }
    }

    //countdown.
    private IEnumerator Countdown()
    {
        // Subtly guide the monster towards Charging room
        //monster.AlertAndGuideMonsterToLocation();

        if (RandomizeBool()) // <-- Optionally, give player 1/3 chance to NOT alert the Monster
        {
            monster.AlertAndGuideMonsterToLocation();
        }

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

    private bool RandomizeBool()
    {
        int num = Random.Range(0, 2);
        if (num >= 1) { return true; }
        else { return false; }
    }

}
