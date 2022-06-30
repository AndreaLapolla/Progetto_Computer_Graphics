using System.Collections;
using UnityEngine;

// classe per definire le funzionalità specifiche del ronin
public class Ronin : MeleeCharacter
{
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    private TrailRenderer _trailRenderer;
    
    protected override void Awake()
    {
        base.Awake();
        
        Rigidbody2D.gravityScale = 7;
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    // l'implementazione è un po diversa in quanto non verrà considerato il salto a parete
    protected override void Update()
    {
        if (IsDashing)
        {
            return;
        }
        
        base.Update();
        
        // movimento a destra o a sinistra a seconda se si preme D oppure A
        Rigidbody2D.velocity = new Vector2(HorizontalInput * speed, Rigidbody2D.velocity.y);

        // per consentire al ronin di effettuare un altro doppio salto solo dopo essere tornato a terra
        if (IsGrounded())
        {
            canDoubleJump = true;
        }
        
        // alla pressione della barra spaziatrice
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            SoundManager.Instance.PlaySound(jumpSound);
        }
        
        // implementazione dash
        if (Input.GetMouseButtonDown(1) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    
    // implementazione del dash
    private IEnumerator Dash()
    {
        SoundManager.Instance.PlaySound(jumpSound);
        canDash = false;
        IsDashing = true;
        float originaGravity = Rigidbody2D.gravityScale;
        Rigidbody2D.gravityScale = 0f;
        Rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        _trailRenderer.emitting = false;
        Rigidbody2D.gravityScale = originaGravity;
        IsDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
