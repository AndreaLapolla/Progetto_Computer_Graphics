using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpCooldown;
    private float horizontalInput;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpPower = 10;
    
    [SerializeField] private float MaxHealth = 10;
    [SerializeField] private float MaxStamina = 10;
    [SerializeField] private float MaxMana = 10;

    private void Awake()
    {
        // inizializzazione oggetti importanti
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // valore che indica se il personaggio si sta muovendo a destra o sinistra
        horizontalInput = Input.GetAxis("Horizontal");

        // implementazione rotazione quando si cambia direzione
        if (horizontalInput > 0.01f)
        {
            // transform.localScale = Vector3.one;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }
        else if (horizontalInput < -0.01f)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }

        // per stabuilire quale animazione attivare
        anim.SetBool("running", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // i movimenti sono bloccati durante il cooldown del salto
        if (wallJumpCooldown < 0.2f)
        {
            // movimento a destra o a sinistra a seconda se si preme D oppure A
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // se il personaggio si aggrappa alla parete
            if (onWall() && !isGrounded())
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
    }

    // implementazione salto
    private void Jump()
    {
        // salto da terra
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            // attiva l'animazione del salto
            anim.SetTrigger("jump");
        }
        // implementazione salto a parete
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
        }
    }

    // codice da eseguire alla collisione con oggetti dal tag Ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    // restituisce true se il personaggio è a terra
    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }
    
    // restituisce true se il perosnaggio tocca il muro
    private bool onWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }

    // restituisce true se il personaggio è in condizione di attaccare
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
