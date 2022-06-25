using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// classe per definire le funzionalità specifiche dell'arciere
public class Archer : Character
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    
    // in questa classe l'attacco è a distanza
    protected override void Attack()
    {
        base.Attack();

        arrows[0].transform.position = firePoint.position;
        arrows[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    
    protected override void Jump()
    {
        base.Jump();
        
        // implementazione salto a parete
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
