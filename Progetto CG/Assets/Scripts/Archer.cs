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
}
