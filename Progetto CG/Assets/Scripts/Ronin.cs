using System.Collections;
using UnityEngine;

// classe per definire le funzionalità specifiche del ronin
public class Ronin : Character
{
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    private TrailRenderer trailRenderer;
    
    protected override void Awake()
    {
        base.Awake();
        
        body.gravityScale = 7;
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // l'implementazione è un po diversa in quanto non verrà considerato il salto a parete
    protected override void Update()
    {
        if (isDashing)
        {
            return;
        }
        
        base.Update();
        
        // movimento a destra o a sinistra a seconda se si preme D oppure A
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // per consentire al ronin di effettuare un altro doppio salto solo dopo essere tornato a terra
        if (IsGrounded())
        {
            canDoubleJump = true;
        }
        
        // alla pressione della barra spaziatrice
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
        // todo bisogna gestire il danno, come in warrior
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
