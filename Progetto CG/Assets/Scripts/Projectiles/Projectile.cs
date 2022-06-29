using UnityEngine;

// classe per controllare gli attacchi a distanza
public class Projectile : MonoBehaviour
{
    [Header("Projectile Movement")]
    [SerializeField] private float speed;
    
    private float _damage;
    private bool _hit;
    private float _direction;
    private float _lifeTime;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // caso in cui il proiettile impatti con qualcosa
        if(_hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);
        _lifeTime += Time.deltaTime;
        
        // dopo troppo tempo in aria il proiettile scompare
        if (_lifeTime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    // funzione attivata al contatto del proiettile con qualcosa
    private void OnTriggerEnter2D(Collider2D col)
    {
        _hit = true;
        _boxCollider.enabled = false;

        if (col.tag == "Enemy")
        {
            col.GetComponent<Health>().TakeDamage(_damage);
        }
        
        Deactivate();
    }

    // funzione che permettere al proiettile di avere la direzione del personaggio nel momento in cui spara
    public void SetDirection(float direction, float damage)
    {
        _lifeTime = 0;
        _direction = direction;
        _damage = damage;
        gameObject.SetActive(true);
        _hit = false;
        _boxCollider.enabled = true;
        float localScaleX = transform.localScale.x;
        
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
