using UnityEngine;

// classe per gestire il comportamento generale del boss
public class DeathBringerIdle : StateMachineBehaviour
{
    [Header("Boss Movement Parameters")]
    [SerializeField] private float meleeAttackRange = 3f;
    [SerializeField] private float meleeAttackCoolDown = 2f;
    [SerializeField] private float spellAttackRange = 10f;
    [SerializeField] private float spellAttackCoolDown = 3f;
    
    private Transform _playerTransform;
    private Rigidbody2D _rigidbody2D;
    private Boss _boss;
    private float _cooldwnTimer;
    
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

        _cooldwnTimer += Time.deltaTime;
        
        if (Vector2.Distance(_playerTransform.position, _rigidbody2D.position) >= spellAttackRange)
        {
            if (_cooldwnTimer >= spellAttackCoolDown)
            {
                // condizione di attacco magico
                animator.SetTrigger("spell");
                _cooldwnTimer = 0;
            }
        }
        else
        {
            if (Vector2.Distance(_playerTransform.position, _rigidbody2D.position) > meleeAttackRange)
            {
                // condizione di per iniziare a muoversi verso il giocatore
                animator.SetBool("moving", true);
            }
            else
            {
                if (_cooldwnTimer >= meleeAttackCoolDown)
                {
                    // condizione di attacco in mischia
                    animator.SetTrigger("meleeAttack");
                    _cooldwnTimer = 0;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("meleeAttack");
        animator.ResetTrigger("spell");
    }
}
