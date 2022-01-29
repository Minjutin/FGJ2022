using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_character_movement : MonoBehaviour
{
    Rigidbody playerRB;

    [Header("Muuttujat")]
    [SerializeField] float moveSpeed = 5f;


    // Vectorit
    Vector3 moveDirection;
    Vector3 sideDirection;

    // Boolit
    private bool movingForward;
    private bool movingBack;
    private bool movingLeft;
    private bool movingRight;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        CheckMoveInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckMoveInput()
    {

        #region Movement Input Handling

        // Checks if Player is moving forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            movingForward = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            movingForward = false;
        }
        // Checks if Player is moving back, and so on...
        if (Input.GetKeyDown(KeyCode.S))
        { movingBack = true; }
        if (Input.GetKeyUp(KeyCode.S))
        { movingBack = false; }

        if (Input.GetKeyDown(KeyCode.A))
        { movingLeft = true; }
        if (Input.GetKeyUp(KeyCode.A))
        { movingLeft = false; }

        if (Input.GetKeyDown(KeyCode.D))
        { movingRight = true; }
        if (Input.GetKeyUp(KeyCode.D))
        { movingRight = false; }


        #endregion
    }

    private void Move()
    {
        moveDirection = gameObject.transform.forward;
        sideDirection = gameObject.transform.right;

        if (movingForward)
        {
            playerRB.AddForce(moveDirection * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        if (movingBack)
        {
            playerRB.AddForce(-moveDirection * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }

        if (movingLeft)
        {
            playerRB.AddForce(-sideDirection * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        if (movingRight)
        {
            playerRB.AddForce(sideDirection * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}