using UnityEngine;

// classe per gestire il danno inflitto dalle trappole
public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
