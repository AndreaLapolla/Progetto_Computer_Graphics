using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// classe di base del personaggio giocabile
// todo implementare dash
public class Character : MonoBehaviour
{
    protected Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Character playerMovement;

    protected float wallJumpCooldown;
    protected float horizontalInput;
    private float cooldownTimer = Mathf.Infinity;
    protected int currentJumps = 0;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed;
    [SerializeField] protected float jumpPower;
    [SerializeField] private float attackCooldown;
    [SerializeField] protected int maxNumJumps;

    private void Awake()
    {
        // inizializzazione oggetti importanti
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<Character>();
    }

    // todo implementazione dash (servirà un nuovo tasto di input)
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
        anim.SetBool("grounded", IsGrounded());

        // i movimenti sono bloccati durante il cooldown del salto
        if (wallJumpCooldown < 0.2f)
        {
            // movimento a destra o a sinistra a seconda se si preme D oppure A
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // se il personaggio si aggrappa alla parete
            if (OnWall() && !IsGrounded())
            {
                ManageWallGrabbing();
                
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
        
        // reazione al click del tasto sinistro del mouse per attaccare
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (CompareTag("Ground"))
        {
            currentJumps = 0;
        }
    }

    // restituisce true se il personaggio è a terra
    protected bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, 
            boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }
    
    // restituisce true se il perosnaggio tocca il muro
    protected bool OnWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, 
            boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 
            0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }

    // restituisce true se il personaggio è in condizione di attaccare
    private bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded() && !OnWall();
    }
    
    // funzione di attacco, da sovrascrivere nelle classi specializzate
    // sarà uguale per ronin e guerriero ma non per l'arciere in quanto a distanza
    protected virtual void Attack()
    {
        // codice base uguale per tutte le classi figlie
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }
    
    // funzione di salto, da sovrascrivere nelle classi specializzate
    // sarà uguale per l'arciere e il guerriero mentre il ronin potrà eseguire il doppio salto ma non il
    // salto a parete
    protected virtual void Jump()
    {
        // salto da terra oppure 
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            // attiva l'animazione del salto
            anim.SetTrigger("jump");
            currentJumps++;
        }
    }

    protected virtual void ManageWallGrabbing()
    {
        body.gravityScale = 0;
        body.velocity = Vector2.zero;
    }
}
