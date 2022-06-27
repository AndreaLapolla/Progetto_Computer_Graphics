using System.Collections;
using UnityEngine;

// classe per definire le funzionalità specifiche del guerriero
public class Warrior : Character
{
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    private TrailRenderer _trailRenderer;

    protected override void Awake()
    {
        base.Awake();

        _trailRenderer = GetComponent<TrailRenderer>();
    }

    protected override void Update()
    {
        if (IsDashing)
        {
            return;
        }
        
        base.Update();
        
        // i movimenti sono bloccati durante il cooldown del salto
        if (WallJumpCooldown < 0.2f)
        {
            // movimento a destra o a sinistra a seconda se si preme D oppure A
            Rigidbody2D.velocity = new Vector2(HorizontalInput * speed, Rigidbody2D.velocity.y);

            // se il personaggio si aggrappa alla parete
            if (OnWall() && !IsGrounded())
            {
                Rigidbody2D.gravityScale = 0;
                Rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                Rigidbody2D.gravityScale = 7;
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
            WallJumpCooldown += Time.deltaTime;
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
            if (HorizontalInput == 0)
            {
                Rigidbody2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(
                    -Mathf.Sign(transform.localScale.x) * Mathf.Abs(transform.localScale.x), 
                    transform.localScale.y, transform.localScale.z);
            }
            else
            {
                Rigidbody2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            WallJumpCooldown = 0;
        }
    }

    // implementazione del dash
    private IEnumerator Dash()
    {
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
