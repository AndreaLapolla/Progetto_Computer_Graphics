using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    protected override void Attack()
    {
        print("attack");
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }
}
