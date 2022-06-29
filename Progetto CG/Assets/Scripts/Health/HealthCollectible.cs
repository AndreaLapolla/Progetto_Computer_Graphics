using UnityEngine;

// classe per gestire gli oggetti curativi
public class HealthCollectible : MonoBehaviour
{
    [Header("Collectible Healing Value")]
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
