using UnityEngine;

// classe per gestire i movimenti del personaggio
public class DeathBringerRun : StateMachineBehaviour
{
    [Header("Boss Movement Parameters")]
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float meleeAttackRange = 3f;
    [SerializeField] private float spellAttackRange = 10f;
    
    private Transform _playerTransform;
    private Rigidbody2D _rigidbody2D;
    private Boss _boss;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody2D = animator.GetComponent<Rigidbody2D>();
        _boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss.LookAtPlayer();
        
        // movimento verso il giocatore
        Vector2 target = new Vector2(_playerTransform.position.x, _rigidbody2D.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_rigidbody2D.position, target, 
            speed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(newPosition);
        
        // condizione di attacco
        if (Vector2.Distance(_playerTransform.position, _rigidbody2D.position) <= meleeAttackRange 
            || 
            Vector2.Distance(_playerTransform.position, _rigidbody2D.position) >= spellAttackRange)
        {
            animator.SetBool("moving", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
