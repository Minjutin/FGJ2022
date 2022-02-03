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

    public void PutLightsInstantlyOn()
    {
        animator.SetBool("InstantOn", true);
        animator.SetBool("lightsOn", true);
        StartCoroutine(DelayedResetBool());
    }

    IEnumerator DelayedResetBool()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("InstantOn", false);
    }
}
