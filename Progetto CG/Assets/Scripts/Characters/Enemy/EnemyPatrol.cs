using UnityEngine;
using UnityEngine.Serialization;

// classe per gestire la sorveglianza
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    
    [Header("Enemy")] 
    [SerializeField] private Transform enemy;
    
    [Header("Movement Parameters")] 
    [SerializeField] private float speed;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;

    [FormerlySerializedAs("_animator")]
    [Header("Animator")]
    [SerializeField] private Animator animator;

    private Vector3 _initScale;
    private bool _movingLeft;
    private float _idleTimer;
    
    private void Awake()
    {
        _initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }

    // in update si controlla quando il nemico ha incontrato uno dei due limiti e in tal caso
    // deve cambiare direzione
    private void Update()
    {
        if (_movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
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
            if (enemy.position.x <= rightEdge.position.x)
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
        
        animator.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction, _initScale.y, _initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, 
            enemy.position.y, enemy.position.z);
    }

    // gestione del passaggio da running a idle
    private void DirectionChange()
    {
        animator.SetBool("moving", false);
        
        _idleTimer += Time.deltaTime;
        if (_idleTimer > idleDuration)
        {
            _movingLeft = !_movingLeft;
        }
    }
}
