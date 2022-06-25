using System.Collections;
using UnityEngine;

// classe per definire le funzionalità specifiche del guerriero
public class Warrior : Character
{
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    private TrailRenderer trailRenderer;

    protected override void Awake()
    {
        base.Awake();

        trailRenderer = GetComponent<TrailRenderer>();
    }

    protected override void Update()
    {
        if (isDashing)
        {
            return;
        }
        
        base.Update();
        
        // i movimenti sono bloccati durante il cooldown del salto
        if (wallJumpCooldown < 0.2f)
        {
            // movimento a destra o a sinistra a seconda se si preme D oppure A
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // se il personaggio si aggrappa alla parete
            if (OnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
            }
            
            // alla pressione della barra spaziatrice
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            // si aggiunge tempo di ricarica al salto
            wallJumpCooldown += Time.deltaTime;
        }
        
        // implementazione dash
        if (Input.GetMouseButtonDown(1) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    
    // implementazione attacco in mischia
    protected override void Attack()
    {
        base.Attack();
        // todo bisogna gestire il danno
    }

    // implementazione salto singolo e a parete
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

    // implementazione del dash
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originaGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        body.gravityScale = originaGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
