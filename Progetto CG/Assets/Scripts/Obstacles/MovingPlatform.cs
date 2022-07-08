using UnityEngine;

// classe per gestire il movimento di una piattaforma, sia in verticale che in orizzontale
public class MovingPlatform : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Transform limit1Transform;
    [SerializeField] private Transform limit2Transform;
    
    [Header("Movement Parameters")] 
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;

    [Header("Movement Type")] 
    [SerializeField] private string movement;
    [SerializeField] private bool movingBack;
    
    private float _idleTimer;
    private Transform _platformTransform;

    private void Awake()
    {
        _platformTransform = gameObject.transform;
    }

    private void Update()
    {
        switch (movement)
        {
            case "horizontal":
                MoveHorizontally();
                break;
            case "vertical":
                MoveVertically();
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }

    private void MoveHorizontally()
    {
        if (movingBack)
        {
            if (_platformTransform.position.x >= limit1Transform.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (_platformTransform.position.x <= limit2Transform.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void MoveVertically()
    {
        if (movingBack)
        {
            if (_platformTransform.position.y >= limit1Transform.position.y)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (_platformTransform.position.y <= limit2Transform.position.y)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    // funzione per controllare i movimenti del nemico
    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;

        switch (movement)
        {
            case "horizontal":
                _platformTransform.position = new Vector3(_platformTransform.position.x + Time.deltaTime 
                    * direction * speed, _platformTransform.position.y, _platformTransform.position.z);
                break;
            case "vertical":
                _platformTransform.position = new Vector3(_platformTransform.position.x, 
                    _platformTransform.position.y + Time.deltaTime * direction * speed, 
                    _platformTransform.position.z);
                break;
        }
    }

    private void DirectionChange()
    {
        _idleTimer += Time.deltaTime;
        if (_idleTimer > idleDuration)
        {
            movingBack = !movingBack;
        }
    }

    // funzioni per gestire il movimento del personaggio insieme alla piattaforma
    private void OnCollisionEnter2D(Collision2D col)
    {
        col.transform.SetParent(_platformTransform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
