using UnityEngine;

public class Enemy : Entity
{

    protected override void Update()
    {
        HandleCollision();
        HandleAnimations();
        HandleMovement();
        HandleFlip();
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocity.y);
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}
