using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// classe per definire le funzionalità specifiche del guerriero
public class Warrior : Character
{
    protected override void Attack()
    {
        base.Attack();
        // todo bisogna gestire il danno
    }

    protected override void Jump()
    {
        base.Jump();
        // implementazione salto a parete
        // c'era un else
        if (OnWall() && !IsGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(
                    -Mathf.Sign(transform.localScale.x) * Mathf.Abs(transform.localScale.x), 
                    transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
        }
    }
}
