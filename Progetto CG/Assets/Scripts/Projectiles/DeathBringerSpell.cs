using UnityEngine;

// classe per gestire l'attacco magico del boss death bringer, diverso dalle freccie
public class DeathBringerSpell : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    
    [Header("Spell Attack Parameters")]
    [SerializeField] private float spellAttackRange;
    [SerializeField] private float colliderDistance;
    
    
    private float _spellAttackDamage;
    private LayerMask _playerMask;

    // chiamato da funzione per disattivare l'oggetto
    public void DeactivateSpell()
    {
        gameObject.SetActive(false);
    }
    
    // chiamato da funzione per infliggere danno magico, chiamata da animazione
    public void DealSpellDamage()
    {
        // si ricerca il personaggio all'interno dell'area definita e si infligge il danno magico
        RaycastHit2D hit2D = Physics2D.BoxCast(boxCollider2D.bounds.center - transform.up * 
            spellAttackRange * transform.localScale.y * colliderDistance, 
            new Vector3(boxCollider2D.bounds.size.x, boxCollider2D.bounds.size.y * spellAttackRange, 
                boxCollider2D.bounds.size.z), 0, Vector2.down, 0, _playerMask);
        if (hit2D.collider != null)
        {
            hit2D.transform.GetComponent<Health>().TakeDamage(_spellAttackDamage);
        }
    }

    // funzione chiamata per attivare l'animazione principale
    public void SpellAttack(int spellAttackDamage, LayerMask playerMask)
    {
        animator.SetTrigger("spell");
        _spellAttackDamage = spellAttackDamage;
        _playerMask = playerMask;
    }
    
    // funzione per mostrare l'area di attacco
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center - transform.up * spellAttackRange * 
            transform.localScale.y * colliderDistance, new Vector3(boxCollider2D.bounds.size.x, 
                boxCollider2D.bounds.size.y * spellAttackRange, boxCollider2D.bounds.size.z));
    }
}
