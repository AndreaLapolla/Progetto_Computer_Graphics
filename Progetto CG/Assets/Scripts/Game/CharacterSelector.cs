using UnityEngine;

// classe che determina il personaggio da usare
public class CharacterSelector : MonoBehaviour
{
    [Header("Characters Components")]
    [SerializeField] private GameObject warrior;
    [SerializeField] private GameObject archerParent;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject ronin;
    
    [Header("Stringa Provvisoria")]
    [SerializeField] private string chosenCharacter;

    private void Awake()
    {
        // si disattivano tutti i personaggi per assicurarci che non se attivino di più nella stessa partita
        warrior.SetActive(false);
        archerParent.SetActive(false);
        ronin.SetActive(false);
    }

    // restituisce il personaggio da usare
    public GameObject GetPlayedCharacter()
    {
        GameObject player;
        switch (chosenCharacter)
        {
            case "warrior":
            {
                warrior.SetActive(true);
                player = warrior;
                break;
            }
            case "archer":
            {
                archerParent.SetActive(true);
                player = archer;
                break;
            }
            case "ronin":
            {
                ronin.SetActive(true);
                player = ronin;
                break;
            }
            default:
            {
                print("errore inserimento nome persoanggio, verrà usato warrior di default");
                warrior.SetActive(true);
                player = warrior;
                break;
            }
        }
        return player;
    }
}
