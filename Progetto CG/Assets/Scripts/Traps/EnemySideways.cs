using UnityEngine;

// classe per gestire il danno inflitto dalle trappole spuntoni
public class EnemySideways : Trap
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (trapSounds.Length >= 1)
            {
                SoundManager.Instance.PlaySound(trapSounds[0]);
            }
            col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
