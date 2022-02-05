using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject gunPoint;

    [SerializeField] Vector3 audioPos;
    [SerializeField] AudioClip emptyClick;
    [SerializeField] AudioClip bangBang;

    public bool canBeUsed = false;

    public GameObject projectile;

    private void Start()
    {
        audioPos = FindObjectOfType<AudioManager>().gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //If player decides to shoot.
        if (canBeUsed && Input.GetKeyDown("space"))
        {
            Instantiate(projectile, gunPoint.transform.position, gunPoint.transform.rotation);
            //PUT LOUD SOUND HERE
            AudioSource.PlayClipAtPoint(bangBang, audioPos);

            canBeUsed = false;

        }

        if(!canBeUsed && Input.GetKeyDown("space"))
        {
            //PUT EMPTY SHOOT - SOUND HERE
            AudioSource.PlayClipAtPoint(emptyClick, audioPos);
        }

    }

}
