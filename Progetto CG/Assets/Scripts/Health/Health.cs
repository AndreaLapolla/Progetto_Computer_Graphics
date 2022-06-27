using System.Collections;
using UnityEngine;

// classe per gestire la salute dei personaggi
public class Health : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] private float startingHealth;

    [Header("iFrames")] 
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    private SpriteRenderer _spriteRenderer;

    public float CurrentHealth {get; private set; }
    private Animator _animator;
    private bool _dead;

    private void Awake()
    {
        // all'inizio la salute avrà valora massimo
        CurrentHealth = startingHealth;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // prova
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
    
    // funzione in cui si stabilisce se i danni sono fatali oppure no
    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        
        if (CurrentHealth > 0)
        {
            // sopravvive
            _animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            // muore
            if (!_dead)
            {
                _animator.SetTrigger("die");
                GetComponent<Character>().enabled = false;
                _dead = true;
                // todo ci sarà qualcos'altro da fare per gestire la fine della partita 
            }
            
        }
    }

    // funzione per restituire salute
    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + value, startingHealth);
    }

    // funzione per gestire l'invulnerabilità post danno e generando un effetto lampeggiante
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            _spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
    
    public float GetStartingHealth()
    {
        return startingHealth;
    }
}
