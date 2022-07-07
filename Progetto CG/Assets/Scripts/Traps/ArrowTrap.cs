
using UnityEngine;

// classe per gestire la trappola scaglia-freccie
public class ArrowTrap : Trap
{
    [Header("Arrow Trap Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;

    private float _cooldownTimer;

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        
        if (_cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _cooldownTimer = 0;
        SoundManager.Instance.PlaySound(trapSounds[0]);
        arrows[FindArrow()].transform.position = firepoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
