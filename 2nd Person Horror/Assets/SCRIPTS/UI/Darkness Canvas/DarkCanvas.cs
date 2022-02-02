using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCanvas : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PutTheLightsOut()
    {
        animator.SetBool("lightsOn", false);
    }

    public void PutTheLightsOn()
    {
        animator.SetBool("lightsOn", true);
    }
}
