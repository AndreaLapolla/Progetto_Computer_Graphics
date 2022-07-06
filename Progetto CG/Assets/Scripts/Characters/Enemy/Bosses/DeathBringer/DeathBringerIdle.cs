using UnityEngine;

// classe per gestire il comportamento generale del boss
public class DeathBringerIdle : StateMachineBehaviour
{
    [Header("Boss Movement Parameters")]
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float attackRange = 3f;
    
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
        
        if (Vector2.Distance(_playerTransform.position, _rigidbody2D.position) > attackRange)
        {
            // condizione di per iniziare a muoversi verso il giocatore
            animator.SetBool("moving", true);
            Vector2 target = new Vector2(_playerTransform.position.x, _rigidbody2D.position.y);
            Vector2 newPosition = Vector2.MoveTowards(_rigidbody2D.position, target, 
                speed * Time.fixedDeltaTime);
            _rigidbody2D.MovePosition(newPosition);
        }
        else if (Vector2.Distance(_playerTransform.position, _rigidbody2D.position) <= attackRange)
        {
            // condizione di attacco
            animator.SetBool("moving", false);
            animator.SetTrigger("meleeAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("meleeAttack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
