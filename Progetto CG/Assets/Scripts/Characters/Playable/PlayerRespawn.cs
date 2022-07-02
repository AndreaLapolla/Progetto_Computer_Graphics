using UnityEngine;

// classe per gestire il respawn del personaggio
public class PlayerRespawn : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioClip checkPointSound;

    private Transform _currentCheckpoint;
    private Health _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
    }

    // funzione per far riapparire il personaggio all'ultimo checkpoint
    public void Respawn()
    {
        transform.position = _currentCheckpoint.position;
        _playerHealth.Respawn();
    }

    // funzione per attivare il checkpoint al tocco
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "CheckPoint")
        {
            _currentCheckpoint = col.transform;
            SoundManager.Instance.PlaySound(checkPointSound);
            col.GetComponent<Collider2D>().enabled = false;
            col.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
