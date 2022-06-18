using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// questa classe andrebbe rimossa e il codice andrebbe inserito tra character e archer
/// </summary>
public class RangedAttack : MonoBehaviour
{
    /// <summary>
    /// archer
    /// </summary>
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    
    /// <summary>
    /// fisso in characther
    /// </summary>
    private Animator anim;
    private Archer playerMovement;

    private float cooldownTimer = Mathf.Infinity;

    /// <summary>
    /// fisso in character
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Archer>();
    }

    /// <summary>
    /// fisso in character
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    /// <summary>
    /// archer
    /// </summary>
    private void Attack()
    {
        print("attack");
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        arrows[0].transform.position = firePoint.position;
        arrows[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
