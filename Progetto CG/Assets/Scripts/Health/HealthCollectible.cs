using UnityEngine;

// classe per gestire gli oggetti curativi
public class HealthCollectible : MonoBehaviour
{
    [Header("Sounds")] 
    [SerializeField] private AudioClip pickUpSound;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            SoundManager.Instance.PlaySound(pickUpSound);
            col.GetComponent<Health>().RefillHealth();
            gameObject.SetActive(false);
        }
    }
}
