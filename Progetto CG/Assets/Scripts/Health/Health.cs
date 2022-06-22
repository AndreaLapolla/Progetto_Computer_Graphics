using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// classe per gestire la salute dei personaggi
public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth {get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
    
    // funzione in cui si stabilisce se i danni sono fatali oppure no
    private void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            // sopravvive
            anim.SetTrigger("hurt");
        }
        else
        {
            // muore
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<Character>().enabled = false;
                dead = true;
            }
            
        }
    }

    public float GetStartingHealth()
    {
        return startingHealth;
    }
}
