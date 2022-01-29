using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPathfinding : MonoBehaviour
{
    public NavMeshAgent agent;
    [Header("Targeting")]
    public Vector3 currentTargetPos;  //<-- SAME AS BELOW
    public GameObject currentTarget;
    [SerializeField] GameObject startingTarget;
    public bool shouldIMove;

    // Muuttujat
    [Header("Variables")]
    private float targetingTimer = 0.5f;
    public bool timeForTargetCheck = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Sets the first Target up
        currentTarget = startingTarget;
        currentTargetPos = startingTarget.transform.position;
    }


    void Update()
    {
        Move();
        if (timeForTargetCheck)
        {
            StartCoroutine(CheckTargeting());
        }
    }

    private void Move()
    {
        if (shouldIMove)
        {
            agent.SetDestination(currentTargetPos);
        }
    }

    IEnumerator CheckTargeting()
    {
        timeForTargetCheck = false;
        Debug.Log("targeting");

        // Check if Location of Monster and pathway Target is the same
        if (HasReachedCurrentTarget())
        {
            // Get the next Target
            currentTarget = currentTarget.GetComponent<Target>().GiveNextTarget();
            currentTargetPos = currentTarget.transform.position;
            Debug.Log("NEW TARGET ACQUIRED");
        }

        yield return new WaitForSeconds(targetingTimer);
        timeForTargetCheck = true;
    }

    private bool HasReachedCurrentTarget()
    {
        bool targetReached = true;
        if (transform.position == currentTargetPos)
        {
            targetReached = true;
        }
        else { targetReached = false; }
        return targetReached;
    }
}
