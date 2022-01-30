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
    FieldOfView fov;
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
        fov = FindObjectOfType<FieldOfView>();
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
            //if (isHunting)
            //{
            //    agent.SetDestination(lastKnownPlayerPosition);
            //}
            agent.SetDestination(currentTargetPos);
        }


        //agent.SetDestination(GetPositionWithoutY(player));
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
                //currentTarget = null;        // <-- Aactivate later
                currentTargetPos = lastKnownPlayerPosition;
            }
            else // IF Monster has reached last known Player Position
            {
                // Do the search (look around animation) for a duration

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
        //var selfPos = new Vector3(transform.position.x, 0f, transform.position.z);
        var selfPos = GetPositionWithoutY(gameObject);

        //var targetPos = new Vector3(currentTarget.transform.position.x, 0f, currentTarget.transform.position.z); // <-- Works
        //var targetPos = new Vector3(currentTargetPos.x, 0f, currentTargetPos.z);
        var targetPos = GetPositionWithoutY(currentTarget);

        if (selfPos == targetPos)
        {
            targetReached = true;
        }
        else { targetReached = false; }
        return targetReached;
    }

    private Vector3 GetPositionWithoutY(GameObject pos)
    {
        var desiredPos = new Vector3(pos.transform.position.x, 0f, pos.transform.position.z);
        return desiredPos;
    }

    private void CheckIfHunting()
    {
        isOnSight = fov.CheckTheVisibleTargetListForPlayer();

        // Check if you see the Player
        if (isOnSight && isHunting)
        {
            isOnSight = true;
            lastKnownPlayerPosition = GetPositionWithoutY(player);
        }
        else if (isOnSight && !isHunting)
        {
            EnterHuntingMode();
        }
        else if (!isOnSight && isHunting)
        {
            //Lost track of Player
            // -> check if you have reached lastKnownLocation

            //LostSightOfPlayer();
        }

    }

    private void EnterHuntingMode()
    {
        // Do the "staredown" animation
        // --> change the speed to minimal (agent will turn but not move fast then)

        isHunting = true;

        // Change Monster Speed to higher
        agent.speed = huntingSpeed;
    }

    private void ExitHuntingMode()
    {
        isHunting = false;

        // Change Monster speed to normal
        agent.speed = normalSpeed;

    }

    private void LostSightOfPlayer()
    {


        // Do the search animation

        // Wait for a bit

        // Pick a new waypointTarget
        ExitHuntingMode();

    }
}
