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
    //GameObject startingTarget;
    public bool shouldIMove;

    // Muuttujat
    [Header("Variables")]
    [SerializeField] float targetingTimer = 0.1f;
    public bool timeForTargetCheck = true;

    [Header("Hunting")]
    [SerializeField] GameObject player;
    FieldOfView fov;
    DarknessManager darkness;
    AttackScript attack;

    private Vector3 lastKnownPlayerPosition;

    [SerializeField] bool isOnSight = false;
    [SerializeField] bool isHunting;

    bool stopUpdatingPlayerPos = false;
    bool countdownHasBegun = false;

    [SerializeField] float ORIGINALstaredownTime = 3f;
    public float staredownTime;
    [SerializeField] float lingeringHuntTime = 5f;
    public bool stillUpdatingPlayerPos = false;

    //[Header("Alerted")]
    private bool alerted = false;

    [Header("Speeds")]
    [SerializeField] float normalSpeed = 3.5f;
    [SerializeField] float huntingSpeed = 9f;

    //AUDIO
    AudioSource heartBeat;
    private AudioManager audioM;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfindingManager = FindObjectOfType<PathfindingManager>();

        // Sets the first Target up

        currentTarget = pathfindingManager.FetchStartingTarget();
        currentTargetPos = currentTarget.transform.position;

        // Hunting
        fov = FindObjectOfType<FieldOfView>();
        attack = FindObjectOfType<AttackScript>();
        darkness = FindObjectOfType<DarknessManager>();
        attack.attackCollision = false;
        isHunting = false;

        staredownTime = ORIGINALstaredownTime;

        //Audio
        heartBeat = GetComponent<AudioSource>();    // Shuts out the Heartbeat before sighting
        heartBeat.enabled = false;
        audioM = FindObjectOfType<AudioManager>();
    }


    void Update()
    {
        CheckIfHunting();
        Move();

        if (timeForTargetCheck)
        {
            StartCoroutine(CheckTargeting());
        }

        if (Input.GetKeyDown(KeyCode.Q)) // MANUAL ALERT
        {
            AlertAndGuideMonsterToLocation();
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

                // lingeringHuntTime controls the delay
                currentTargetPos = lastKnownPlayerPosition;
            }
            else // IF Monster has reached last known Player Position
            {
                //ExitHuntingMode();
                // Do the search (look around animation) for a duration

                // Stop Hunting
                ExitHuntingMode();
                //isHunting = false;

                //// Start wandering again

                //currentTarget = startingTarget;
                //currentTargetPos = startingTarget.transform.position; // <-- Temporary reset
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
        Vector3 targetPos;
        if (isHunting)
        {
            // lingeringHuntTime float controls the delay for how long long after the Monster knows playerPos after sight is lost
            targetPos = lastKnownPlayerPosition;
        }
        else
        {
            if (!alerted)
            {
                // Normaali ohjaus
                targetPos = GetPositionWithoutY(currentTarget);
            }
            else
            {
                // Purkkakoodi "alerted" ohjaus
                targetPos = currentTargetPos;
            }

            
        }

        if (selfPos == targetPos)
        {
            targetReached = true;
            alerted = false;
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
            stopUpdatingPlayerPos = false;
            countdownHasBegun = false;
            StopCoroutine(LingeringHuntStop());

            // Updates the PlayerPos;
            lastKnownPlayerPosition = GetPositionWithoutY(player);
        }
        else if (isOnSight && !isHunting)
        {
            EnterHuntingMode(staredownTime);
        }
        else if (!isOnSight && isHunting)
        {

            // Keep updating the lastKnowPlayerPosition for lingeringHuntTime amount of time
            if (!stopUpdatingPlayerPos)
            {
                lastKnownPlayerPosition = GetPositionWithoutY(player);
            }

            // Begin countdown for how long will keep updating lastKnownPlayerPos
            if (!countdownHasBegun)
            {
                StartCoroutine(LingeringHuntStop());
            }




            // CHECK CheckTargeting for ExitHuntingMode();
        }

    }

    public void EnterHuntingMode(float _staredownTime)
    {
        isHunting = true;

        // Get the lights back on
        darkness.LightsOn();

        //Play audio
        audioM.MonsterSawYou();
        // Enable heartbeat
        heartBeat.enabled = true;

        staredownTime = _staredownTime;
        StartCoroutine(BeginTheHunt());
    }

    IEnumerator BeginTheHunt()
    {
        StopCoroutine(BeginTheHunt());

        // Do the "staredown" animation
        // --> change the speed to minimal (agent will turn but not move fast then)

        // Drop the speed
        agent.speed = 0.5f;
        yield return new WaitForSeconds(staredownTime);

        // reset Staredown Time
        staredownTime = ORIGINALstaredownTime;

        // Stop the "staredown"

        // Activate the Attack Hit Box
        attack.attackCollision = true;

        // Change Monster Speed to higher
        agent.speed = huntingSpeed;
    }

    private void ExitHuntingMode()
    {
        isHunting = false;

        // Disable Heartbeat
        heartBeat.enabled = false;

        // Deactivate Attack Hit Box
        attack.attackCollision = false;

        // Change Monster speed to normal
        agent.speed = normalSpeed;

        // Reset rail patrol
        
        //currentTarget = startingTarget;
        //currentTargetPos = startingTarget.transform.position;

        // Randomize Reset waypoint Target
        RandomizeWaypointTarget();
    }

    public bool GetHuntingMode()
    {
        return isHunting;
    }

    public void AlertAndGuideMonsterToLocation()
    {
        // Shut down the lights
        darkness.LightsOut();

        // Turn off the last waypoint Target?

        // Turn off alerte
        alerted = true;

        // Turn the Monster towards Player's Position
        StartCoroutine(WaitAndTurn());
    }

    IEnumerator WaitAndTurn()
    {
        //Waits for the lights to go out before turning :)
        yield return new WaitForSeconds(2f);
        // Turn the Monster towards Player's Position
        currentTargetPos = GetPositionWithoutY(player);
    }

    private void RandomizeWaypointTarget()
    {
        currentTarget = pathfindingManager.GetRandomDefaultTarget();
        currentTargetPos = currentTarget.transform.position;
    }

    IEnumerator LingeringHuntStop()
    {
        StopCoroutine(LingeringHuntStop());
        countdownHasBegun = true;

        isHunting = true;
        yield return new WaitForSeconds(lingeringHuntTime);

        stopUpdatingPlayerPos = true;
        //ExitHuntingMode();
    }

    public void SwitchNavMeshAgent(bool powerOn)
    {
        if (powerOn)
        {
            agent.enabled = true;
        }
        else { agent.enabled = false; }
    }
}
