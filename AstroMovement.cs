using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private float dirX;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float runSpeed = 10f;

    private enum MovementState
    {
        idleDown, idleSide, duckDown, duckSide, crawl, walk, run, jump, slide
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButton("Fire1"))
        {
            rb.velocity = new Vector2(dirX * runSpeed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0)
        {
            state = MovementState.walk;
            sprite.flipX = true;
        }

        else if (dirX < 0)
        {
            state = MovementState.walk;
            sprite.flipX = false;
        }

        else
        {
            state = MovementState.idleSide;
        }

        animator.SetInteger("state", (int)state);
    }
}
