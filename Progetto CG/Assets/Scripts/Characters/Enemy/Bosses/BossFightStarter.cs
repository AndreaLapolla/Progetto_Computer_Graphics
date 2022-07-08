using UnityEngine;

// script per attivare il boss quando si entra nell'area della battaglia
public class BossFightStarter : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossHealthBar;

    [Header("Boss Fight Music")] 
    [SerializeField] private GameObject standardAudioSource;
    [SerializeField] private GameObject bossFightAudioSource;

    private bool _battleStarted;

    private Health _bossHealth;

    private void Awake()
    {
        _bossHealth = boss.GetComponent<Health>();
    }

    private void Update()
    {
        // condizione battaglia terminata
        if (_battleStarted && _bossHealth.CurrentHealth == 0)
        {
            bossHealthBar.SetActive(false);
            bossFightAudioSource.SetActive(false);
            standardAudioSource.SetActive(true);
            _battleStarted = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            boss.SetActive(true);
            bossHealthBar.SetActive(true);
            standardAudioSource.SetActive(false);
            bossFightAudioSource.SetActive(true);
            _battleStarted = true;
        }
    }
}
