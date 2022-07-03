using UnityEngine;

// classe per gestire le cadute mortali
public class MortalFall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            var playerHealth = col.GetComponent<Health>();
            playerHealth.TakeDamage(playerHealth.CurrentHealth);
        }
    }
}
