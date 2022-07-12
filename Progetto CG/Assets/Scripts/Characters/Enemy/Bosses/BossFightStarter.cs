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
        var playerInArena = SearchPlayerForFight();

        if (!playerInArena)
        {
            print("giocatore non nell'arena");
            _battleStarted = false;
            boss.SetActive(false);
            bossHealthBar.SetActive(false);
            bossFightAudioSource.SetActive(false);
            standardAudioSource.SetActive(true);
            leftWall.SetActive(false);
            rightWall.SetActive(false);
        }
        else
        {
            print("giocatore nell'arena");
            if (!_battleStarted)
            {
                print("inizio battaglia");
                // condizione inizio battaglia
                _battleStarted = true;
                boss.SetActive(true);
                bossHealthBar.SetActive(true);
                standardAudioSource.SetActive(false);
                bossFightAudioSource.SetActive(true);
                leftWall.SetActive(true);
                rightWall.SetActive(true);
            }
            else
            {
                print("battaglia in corso");
                if (_playerHealth.CurrentHealth == 0 || _bossHealth.CurrentHealth == 0)
                {
                    print("battaglia finita");
                    // condizione battaglia terminata
                    if (_playerHealth.CurrentHealth == 0)
                    {
                        print("morte personaggio");
                        boss.SetActive(false);
                    }
                    if (_bossHealth.CurrentHealth == 0)
                    {
                        print("morte boss");
                        gameObject.SetActive(false);
                        _battleStarted = false;
                        bossHealthBar.SetActive(false);
                        bossFightAudioSource.SetActive(false);
                        standardAudioSource.SetActive(true);
                    }
                }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(arenaX, arenaY, 0f));
    }
}
