using UnityEngine;

// classe per controllare gli attacchi a distanza
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private bool hit;
    private float direction;
    private float lifeTime;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // caso in cui il proiettile impatti con qualcosa
        if(hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        lifeTime += Time.deltaTime;
        
        // dopo troppo tempo in aria il proiettile scompare
        if (lifeTime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    // funzione attivata al contatto del proiettile con qualcosa
    private void OnTriggerEnter2D(Collider2D col)
    {
        hit = true;
        boxCollider.enabled = false;
        Deactivate();
    }

    // funzione che permettere al proiettile di avere la direzione del personaggio nel momento in cui spara
    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        float localScaleX = transform.localScale.x;
        
        if (Mathf.Sign(localScaleX) != _direction)
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
