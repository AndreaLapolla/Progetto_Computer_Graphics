using UnityEngine;

// classe specializzata per i personaggi che infliggono danno in mischia
public class MeleeCharacter : Character
{
    [Header("Melee Attack Parameters")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    
    private Health _enemyHealth;

    // funzione per stabilire se il nemico è vicino
    private bool EnemyInsight()
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * 
            range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, 
                boxCollider2D.bounds.size.z), 0, Vector2.left, 0, enemyLayer);
        if (hit2D.collider != null)
        {
            _enemyHealth = hit2D.transform.GetComponent<Health>();
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
    
    // funzione richiamata nell'animazione
    private void DamageEnemy()
    {
        if (EnemyInsight())
        {
            _enemyHealth.TakeDamage(damage);
        }
    }
}
