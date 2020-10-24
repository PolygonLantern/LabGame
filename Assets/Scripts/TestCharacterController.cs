using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestCharacterController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform groundCheck; 
    
    private float horizontalMovement = 0f;
    private bool isGrounded;
    public bool AirControl;
    
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;

    //animation references
    private const string PlayerRun = "Player_Run";
    private const string PlayerHurt = "Player_Hurt";
    private const string PlayerJump = "Player_Jump";
    private const string PlayerAttack = "Player_Attack";
    private const string PlayerIdle = "Player_Idle";


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position,
            1 << LayerMask.NameToLayer("Foreground and Platforms"));

        if (isGrounded || AirControl)
        {
            if (horizontalMovement != 0)
            {

                rb.velocity = new Vector2(horizontalMovement * runSpeed, rb.velocity.y);
                animator.Play(PlayerRun);
                spriteRenderer.flipX = horizontalMovement < 0;

            }
            else
            {
                animator.Play(PlayerIdle);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                animator.Play(PlayerJump);
            }
        }
    }
}
