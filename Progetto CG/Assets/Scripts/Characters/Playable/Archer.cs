using UnityEngine;

// classe per definire le funzionalità specifiche dell'arciere
public class Archer : Character
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    
    protected override void Update()
    {
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
    }
    
    // implementazione attacco a distanza
    protected override void Attack()
    {
        base.Attack();

        arrows[0].transform.position = firePoint.position;
        arrows[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
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
}
