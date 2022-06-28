using UnityEngine;

// classe per controllare i nemici che attaccano in mischia
public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;
    
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float _cooldownTimer = Mathf.Infinity;
    private Animator _animator;
    private Health _playerHealth;

    private EnemyPatrol _enemyPatrol;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;

        if (PlayerInsight())
        {
            if (_cooldownTimer >= attackCooldown)
            {
                _cooldownTimer = 0;
                _animator.SetTrigger("meleeAttack");
            }
        }

        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInsight();
        }
    }

    // funzione per stabilire se il giocatore è vicino
    private bool PlayerInsight()
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * 
            range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, 
                boxCollider2D.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if (hit2D.collider != null)
        {
            _playerHealth = hit2D.transform.GetComponent<Health>();
        }
        return hit2D.collider != null;
    }

    // funzione per mostrare l'area di individuazione
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * range * 
            transform.localScale.x * colliderDistance, new Vector3(boxCollider2D.bounds.size.x * range, 
                boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }

    // funzione per gestire l'attacco del nemico al giocatore
    private void DamagePlayer()
    {
        if (PlayerInsight())
        {
            _playerHealth.TakeDamage(damage);
        }
    }
}
