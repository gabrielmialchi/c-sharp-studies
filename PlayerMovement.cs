using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES / REFERENCES
    public Rigidbody2D playerRigidbody2D;
    public Animator animator;
    private Vector2 moveDir;
    public float moveSpeed;
    public float runSpeed;
    public float crawlSpeed;

    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isCrawling;
    [HideInInspector] public bool isDucking;
    #endregion

    #region FUNCTIONS
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region METHODS
    private void InputManagement()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", moveDir.x);
        animator.SetFloat("Vertical", moveDir.y);
        animator.SetFloat("Speed", moveDir.sqrMagnitude);
    }

    private void Move()
    {
        playerRigidbody2D.MovePosition(playerRigidbody2D.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
        PlayerRunning();
        PlayerCrawling();
        PlayerDuck();
    }

    private void PlayerRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerRigidbody2D.MovePosition(playerRigidbody2D.position + moveDir.normalized * runSpeed * Time.fixedDeltaTime);
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void PlayerCrawling()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerRigidbody2D.MovePosition(playerRigidbody2D.position + moveDir.normalized * crawlSpeed * Time.fixedDeltaTime);
            isCrawling = true;
        }
        else
        {
            isCrawling = false;
        }

    }

    private void PlayerDuck()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isDucking = true;
        }
        else
        {
            isDucking = false;
        }
    }
    #endregion
}
