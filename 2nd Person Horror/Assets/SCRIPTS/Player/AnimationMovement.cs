using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMovement : MonoBehaviour
{

    Animator playerAnimator;
    public GameObject playerGraphics;
    // Update is called once per frame
    void Start() 
    {
        playerAnimator = playerGraphics.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            playerAnimator.SetBool("SomethingPressed", true);
            Debug.Log("yes");
        }
        else
        {
            playerAnimator.SetBool("SomethingPressed", false);
        }
    }
}
