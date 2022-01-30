using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPathfinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private PathfindingManager pathfindingManager;
    [Header("Targeting")]
    public Vector3 currentTargetPos;  //<-- SAME AS BELOW
    public GameObject currentTarget;
    private GameObject candidateTarget = null;
    [SerializeField] GameObject startingTarget;
    public bool shouldIMove;

    // Muuttujat
    [Header("Variables")]
    [SerializeField] float targetingTimer = 0.1f;
    public bool timeForTargetCheck = true;

    [Header("Hunting")]
    [SerializeField] GameObject player;
    private Vector3 lastKnownPlayerPosition;

    [SerializeField] bool isOnSight = false;
    [SerializeField] bool isHunting;
    [SerializeField] float normalSpeed = 3.5f;
    [SerializeField] float huntingSpeed = 9f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfindingManager = FindObjectOfType<PathfindingManager>();

        // Sets the first Target up
        currentTarget = startingTarget;
        currentTargetPos = startingTarget.transform.position;

        // Hunting
        isHunting = false;
    }


    void Update()
    {
        CheckIfHunting();
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

        // Check if Location of Monster and pathway Target is the same
        if (!isHunting)
        {

            if (HasReachedCurrentTarget())
            {
                // Compare target list's chosen Target == previousTarget
                bool newTargetFound = false;
                while (newTargetFound == false)
                {
                    candidateTarget = currentTarget.GetComponent<Target>().GiveNextTarget();
                    var previousTarget = pathfindingManager.GetPreviousTarget();
                    if (candidateTarget != previousTarget)
                    {
                        newTargetFound = true;

                        break;
                    }
                    else { newTargetFound = false; }
                }

                pathfindingManager.UpdatePreviousTarget(currentTarget);

                // Sets up the next Target
                currentTarget = candidateTarget;

                currentTargetPos = currentTarget.transform.position;
            }
        }
        else
        {
            // IF HUNTING
            currentTargetPos = lastKnownPlayerPosition;

            // Hunting targeting here
            if (!HasReachedCurrentTarget())
            {
                //currentTarget = null;         <-- Aactivate later
                currentTargetPos = lastKnownPlayerPosition;
            }
            else // IF Monster has reached last known Player Position
            {
                // Stop Hunting
                isHunting = false;

                // Start wandering again
                currentTarget = startingTarget;
                currentTargetPos = startingTarget.transform.position; // <-- Temporary reset
            }
        }

        yield return new WaitForSeconds(targetingTimer);
        timeForTargetCheck = true;
    }

    private bool HasReachedCurrentTarget()
    {
        bool targetReached = true;

        // Gets the positions of both Self and Target without the Y coordinate
        var selfPos = new Vector3(transform.position.x, 0f, transform.position.z);

        //var targetPos = new Vector3(currentTarget.transform.position.x, 0f, currentTarget.transform.position.z); // <-- Works
        var targetPos = new Vector3(currentTargetPos.x, 0f, currentTargetPos.z);

        if (selfPos == targetPos)
        {
            targetReached = true;
        }
        else { targetReached = false; }
        return targetReached;
    }

    private void CheckIfHunting()
    {
        

    }

    private void EnterHuntingMode()
    {
        isHunting = true;

        // Change Monster Speed to higher
    }

    private void ExitHuntingMode()
    {
        isHunting = false;
        // Change Monster speed to normal

    }

    private void LostSightOfPlayer()
    {
        // Keep the last location
        var lastLocation = player.transform.position;
        currentTargetPos = lastLocation;

    }
}
