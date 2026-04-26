using NUnit.Framework.Constraints;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator anim;
    //if you want to keep your variable private but keep showing you the field but keep showing you in the
    //Inspector section(SerializeField)
    [Header("Movement Details")]
    [SerializeField]private float moveSpeed = 3.5f;
    [SerializeField]private float jumpForce = 8;
    private float xInput; //usually it's recommended to make private (unless it's necessary for the game)

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;

    [SerializeField]private bool facingRight = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Find the Rigidbody2D component attached to this same GameObject,
        anim = GetComponentInChildren<Animator>();
        //and store it in the variable rb.
        // it connects your script to the object’s 2D physics body

        
    }

    // Update is called once per frame
    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        HandleMovement(); // it moves only in x direction
                          //rb.linearVelocity.y ==> this means that keep the x - direction provided by me and y direction keep same 

        HandleAnimations(); // 59th line
        HandleFlip();


        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Someone Holding Q key");
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("Someone Realeasing the Q key");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();

        else
        {
            Debug.Log("Nobody is pressing Jump");
        }

    }


    public bool isMoving;

    private void HandleAnimations()
    {
        isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving",isMoving);
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == false)
        {

            Flip();
        } 
        else if(rb.linearVelocity.x < 0 && facingRight == true) {
            Flip();
        }
    }
            

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    private void OnDrawGizmos() // not for actual physics
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance)); //This is the centre of the character(transfom.position)
    }

}
