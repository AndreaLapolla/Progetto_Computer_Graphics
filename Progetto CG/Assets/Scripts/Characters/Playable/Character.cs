using UnityEngine;

// classe di base del personaggio giocabile
public class Character : MonoBehaviour
{
    protected Rigidbody2D Rigidbody2D;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private Character _playerMovement;

    protected float WallJumpCooldown;
    protected float HorizontalInput;
    private float _cooldownTimer = Mathf.Infinity;
    protected bool IsDashing;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpPower;
    [SerializeField] private float attackCooldown;
    [SerializeField] protected bool canDoubleJump;
    [SerializeField] protected bool canDash;

    protected virtual void Awake()
    {
        // inizializzazione oggetti importanti
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _playerMovement = GetComponent<Character>();
    }
    
    protected virtual void Update()
    {
        // valore che indica se il personaggio si sta muovendo a destra o sinistra
        HorizontalInput = Input.GetAxis("Horizontal");

        // implementazione rotazione quando si cambia direzione
        if (HorizontalInput > 0.01f)
        {
            // transform.localScale = Vector3.one;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }
        else if (HorizontalInput < -0.01f)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }

        // istruzione per prevenire la rotazione del personaggio
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        
        // per stabuilire quale animazione attivare
        _animator.SetBool("running", HorizontalInput != 0);
        _animator.SetBool("grounded", IsGrounded());
        
        // reazione al click del tasto sinistro del mouse per attaccare
        if (Input.GetMouseButtonDown(0) && _cooldownTimer > attackCooldown && _playerMovement.CanAttack())
        {
            Attack();
        }
        _cooldownTimer += Time.deltaTime;
    }

    // restituisce true se il personaggio è a terra
    protected bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider.bounds.center, 
            _boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }
    
    // restituisce true se il perosnaggio tocca il muro
    protected bool OnWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider.bounds.center, 
            _boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 
            0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }

    // restituisce true se il personaggio è in condizione di attaccare
    private bool CanAttack()
    {
        return HorizontalInput == 0 && IsGrounded() && !OnWall();
    }
    
    // funzione di attacco, da sovrascrivere nelle classi specializzate
    // sarà uguale per ronin e guerriero ma non per l'arciere in quanto a distanza
    protected virtual void Attack()
    {
        // codice base uguale per tutte le classi figlie
        _animator.SetTrigger("attack");
        _cooldownTimer = 0;
    }
    
    // funzione di salto, da sovrascrivere nelle classi specializzate
    // sarà uguale per l'arciere e il guerriero mentre il ronin potrà eseguire il doppio salto ma non il
    // salto a parete
    protected virtual void Jump()
    {
        // salto da terra oppure 
        if (IsGrounded())
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpPower);
            // attiva l'animazione del salto
            _animator.SetTrigger("jump");
        }
        else
        {
            // codice che implementa il doppio salto, funzionerà solo per il ronin
            if (canDoubleJump)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpPower);
                canDoubleJump = false;
                // attiva l'animazione del salto
                _animator.SetTrigger("jump");
            }
        }
    }
}
