using System;
using UnityEngine;

// classe per gestire il danno inflitto al giocatore
public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
