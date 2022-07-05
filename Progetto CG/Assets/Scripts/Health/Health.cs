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

    [Header("Components")] 
    [SerializeField] private Behaviour[] components;

    [Header("Sounds")] 
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    [Header("Animation Names")] 
    [SerializeField] private string idleAnimationName;
    

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
            SoundManager.Instance.PlaySound(hurtSound);
            StartCoroutine(Invulnerability());
        }
        else
        {
            // muore
            if (!_dead)
            {
                // disattivazione dei componenti
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                
                _animator.SetBool("grounded", true);
                _animator.SetTrigger("die");
                _dead = true;

                if (deathSound != null)
                {
                    SoundManager.Instance.PlaySound(deathSound);
                }
            }
        }
    }
    
    // funzione per restituire salute
    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + value, startingHealth);
    }

    // funzione per implementare il respawn del personaggio
    public void Respawn()
    {
        _dead = false;
        AddHealth(startingHealth);
        _animator.ResetTrigger("die");
        _animator.Play(idleAnimationName);
        StartCoroutine(Invulnerability());
        
        // riattivazione dei componenti
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
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
    
    // funzione attivata da animazione
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
    public float GetStartingHealth()
    {
        return startingHealth;
    }

    public void RefillHealth()
    {
        CurrentHealth = startingHealth;
    }
}
