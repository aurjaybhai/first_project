using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;

    // public Collider2D[] enemyColliders; // fixed size // cannot add or remove items on the go (faster)

    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;

    public List<Collider2D> exampleList; // can have dynamic size (slower)
    //if you want to keep your variable private but keep showing you the field but keep showing you in the
    //Inspector section(SerializeField)
    [Header("Movement Details")]
    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private bool TryToJump;

    protected int facingDir = 1; //Enemy 
    private float xInput; //usually it's recommended to make private (unless it's necessary for the game)
    [SerializeField] private bool facingRight = true;

    protected bool canMove = true;
    private bool canJump = true;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Find the Rigidbody2D component attached to this !!e GameObject,
        anim = GetComponentInChildren<Animator>();
        //and store it in the variable rb.
        // it connects your script to the object’s 2D physics body
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleInput();
        HandleCollision();
        HandleMovement(); // it moves only in x direction
                          //rb.linearVelocity.y ==> this means that keep the x - direction provided by me and y direction keep !!e 

        HandleAnimations(); // 59th line
        HandleFlip();
    }

    public void DamageTargets()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

        foreach (Collider2D enemy in enemyColliders)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();

        }
    }

    private void TakeDamage()
    {
        throw new NotImplementedException();
    }

    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;

        if (!enable) // !!
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            xInput = 0f;
        }
    }


    private void HandleInput()
    {

        xInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryToJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryToAttack();

    }

    private void FixedUpdate()
    {

        if (TryToJump)
        {
            Jump();
            TryToJump = false;
        }
    }

    public bool isMoving;

    protected void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    protected virtual void TryToAttack()
    {
        if (isGrounded)
        {
            EnableMovementAndJump(false); // !!
            anim.SetTrigger("attack");
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }


    private void Jump()
    {
        if (!isGrounded || !canJump)
            return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    protected virtual void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    } // This detects

    protected void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == false)
        {

            Flip();
        }
        else if (rb.linearVelocity.x < 0 && facingRight == true)
        {
            Flip();
        }
    }


    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    private void OnDrawGizmos() // not for actual physics
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance)); //This is the centre of the character(transfom.position)
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

}
