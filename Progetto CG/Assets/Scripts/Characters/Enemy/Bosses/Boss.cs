using UnityEngine;

// classe per gestire i comportamenti fondamentali di un boss
public class Boss : MonoBehaviour
{
    [Header("Character Selector")]
    [SerializeField] private GameObject characterSelector;
    
    [Header("Flip")]
    [SerializeField] private bool isFlipped;

    [Header("Boss Melee Attack")] 
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackRange = 2.9f;
    [SerializeField] private int meleeAttackDamage = 1;
    [SerializeField] private LayerMask playerMask;
    
    [Header("Spell Attack Parameters")]
    [SerializeField] private GameObject spell;
    [SerializeField] private int spellAttackDamage = 1;
    
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
        Vector3 position = transform.position;
        position += transform.right * attackOffset.x;
        position += transform.up * attackOffset.y;

        Collider2D collider2DInfo = Physics2D.OverlapCircle(position, attackRange, playerMask);
        if (collider2DInfo != null)
        {
            collider2DInfo.GetComponent<Health>().TakeDamage(meleeAttackDamage);
        }
    }

    // funzione per gestire l'attacco magico, chiamato da animazione
    public void CastSpell()
    {
        spell.SetActive(true);
        spell.transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1.8f, 
            spell.transform.position.z);
        spell.GetComponent<DeathBringerSpell>().SpellAttack(spellAttackDamage, playerMask);
    }
}
