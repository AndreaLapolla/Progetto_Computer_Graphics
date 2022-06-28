using System;
using UnityEngine;

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
    private Vector3 _initScale;
    private bool _movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float _idleTimer;
    
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        _animator.SetBool("moving", false);
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
        
        _animator.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction, _initScale.y, _initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, 
            enemy.position.y, enemy.position.z);
    }

    // gestione del passaggio da running a idle
    private void DirectionChange()
    {
        _animator.SetBool("moving", false);
        
        _idleTimer += Time.deltaTime;
        if (_idleTimer > idleDuration)
        {
            _movingLeft = !_movingLeft;
        }
    }
}
