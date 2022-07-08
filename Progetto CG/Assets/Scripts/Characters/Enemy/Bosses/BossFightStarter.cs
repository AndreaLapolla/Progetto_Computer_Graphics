using UnityEngine;

// script per attivare il boss quando si entra nell'area della battaglia
public class BossFightStarter : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private LayerMask playerLayer;

    [Header("Arena Dimensions")]
    [SerializeField] private float arenaX = 40;
    [SerializeField] private float arenaY = 10;
    
    [Header("Boss Fight Music")] 
    [SerializeField] private GameObject standardAudioSource;
    [SerializeField] private GameObject bossFightAudioSource;

    private bool _battleStarted;
    private Health _bossHealth;
    private Health _playerHealth;

    private void Awake()
    {
        _bossHealth = boss.GetComponent<Health>();
        boss.SetActive(false);
    }

    private void Update()
    {
        if (!_battleStarted && SearchPlayerForFight())
        {
            // condizione inizio battaglia
            StartBattle();
        }
        else if (_battleStarted && (_bossHealth.CurrentHealth == 0 || _playerHealth.CurrentHealth == 0))
        {
            // condizione battaglia terminata
            _battleStarted = false;
            bossHealthBar.SetActive(false);
            bossFightAudioSource.SetActive(false);
            standardAudioSource.SetActive(true);
            leftWall.SetActive(false);
            rightWall.SetActive(false);
            
            if (_playerHealth.CurrentHealth == 0)
            {
                boss.SetActive(false);
            }
            if (_bossHealth.CurrentHealth == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // funzione che da inizio al combattimento quando il giocatore entra nell'arena
    private bool SearchPlayerForFight()
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, 
            new Vector3(arenaX, arenaY, 0f), 0, new Vector2(), 0, 
            playerLayer);
        if (hit2D.collider != null)
        {
            _playerHealth = hit2D.collider.GetComponent<Health>();
        }
        
        return hit2D.collider != null;
    }

    private void StartBattle()
    {
        boss.SetActive(true);
        bossHealthBar.SetActive(true);
        standardAudioSource.SetActive(false);
        bossFightAudioSource.SetActive(true);
        leftWall.SetActive(true);
        rightWall.SetActive(true);
        _battleStarted = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(arenaX, arenaY, 0f));
    }
}
