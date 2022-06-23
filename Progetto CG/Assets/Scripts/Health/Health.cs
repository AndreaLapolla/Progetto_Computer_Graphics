using UnityEngine;

// classe per gestire la salute dei personaggi
public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth {get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        // all'inizio la salute avrà valora massimo
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
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
    private void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Max(currentHealth - _damage, 0);
        //Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            // sopravvive
            anim.SetTrigger("hurt");
        }
        else
        {
            // muore
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<Character>().enabled = false;
                dead = true;
                // todo ci sarà qualcos'altro da fare per gestire la fine della partita 
            }
            
        }
    }

    public float GetStartingHealth()
    {
        return startingHealth;
    }
}
