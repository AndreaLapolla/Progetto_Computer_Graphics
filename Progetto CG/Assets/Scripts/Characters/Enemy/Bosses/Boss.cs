using UnityEngine;

// classe per gestire i comportamenti fondamentali di un boss
public class Boss : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject characterSelector;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask playerMask;
    
    [Header("Flip")]
    [SerializeField] private bool isFlipped;

    [Header("Boss Melee Attack")]
    [SerializeField] private float meleeAttackRange = 2.9f;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int meleeAttackDamage = 1;
    [SerializeField] private AudioClip meleeAttackSound;
    
    [Header("Spell Attack Parameters")]
    [SerializeField] private GameObject spell;
    [SerializeField] private int spellAttackDamage = 1;
    [SerializeField] private AudioClip spellAttackSound;
    
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = characterSelector.GetComponent<CharacterSelector>().GetPlayedCharacter().transform;
    }

    // funzione per direzionarsi sempre verso il player
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > _playerTransform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < _playerTransform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    // funzione per gestire l'attacco in mischia, chiamato da animazione
    public void MeleeAttack()
    {
        SoundManager.Instance.PlaySound(meleeAttackSound);
        
        RaycastHit2D hit2D = Physics2D.BoxCast(boxCollider2D.bounds.center - transform.right * 
            meleeAttackRange * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider2D.bounds.size.x * meleeAttackRange, boxCollider2D.bounds.size.y, 
                boxCollider2D.bounds.size.z), 0, Vector2.left, 0, playerMask);
        if (hit2D.collider != null)
        {
            hit2D.transform.GetComponent<Health>().TakeDamage(meleeAttackDamage);
        }
    }

    // funzione per gestire l'attacco magico, chiamato da animazione
    public void CastSpell()
    {
        SoundManager.Instance.PlaySound(spellAttackSound);
        spell.SetActive(true);
        spell.transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1.7f, 
            spell.transform.position.z);
        spell.GetComponent<DeathBringerSpell>().SpellAttack(spellAttackDamage, playerMask);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center - transform.right * meleeAttackRange * 
            transform.localScale.x * colliderDistance, new Vector3(boxCollider2D.bounds.size.x * meleeAttackRange, 
                boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }
}
