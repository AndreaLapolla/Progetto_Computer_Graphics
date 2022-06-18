using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    
    protected override void Attack()
    {
        print("attack");
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        arrows[0].transform.position = firePoint.position;
        arrows[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
