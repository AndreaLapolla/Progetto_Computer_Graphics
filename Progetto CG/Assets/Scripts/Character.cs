using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;

    [SerializeField] private float speed = 10;
    [SerializeField] private float MaxHealth = 10;
    [SerializeField] private float MaxStamina = 10;
    [SerializeField] private float MaxMana = 10;

    private bool grounded;

    private void Awake()
    {
        // inizializzazione oggetti importanti
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // movimento a destra o a sinistra a seconda se si preme D oppure A
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // implementazione rotazione quando si cambia direzione
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        // alla pressione della barra spaziatrice
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        
        // per stabuilire quale animazione attivare
        anim.SetBool("running", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    // implementazione salto
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        // attiva l'animazione del salto
        anim.SetTrigger("jump");
        grounded = false;
    }

    // codice da eseguire alla collisione con oggetti dal tag Ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // per tornare all'animazione idle
            grounded = true;
        }
    }
}
