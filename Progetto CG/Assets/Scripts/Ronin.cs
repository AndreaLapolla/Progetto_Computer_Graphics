using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// classe per definire le funzionalità specifiche del ronin
public class Ronin : Character
{
    protected override void Attack()
    {
        base.Attack();
        // todo bisogna gestire il danno, come in warrior
    }
    
    protected override void Jump()
    {
        base.Jump();

        if (Input.GetKey(KeyCode.Space) & !IsGrounded() & currentJumps < maxNumJumps)
        {
            base.Jump();
        }
    }

    // il codice di questa funzione non va eseguito nel caso del ronin perchè lui non effettua il salto a parete
    protected override void ManageWallGrabbing()
    {
        body.gravityScale = 7;
    }
}
