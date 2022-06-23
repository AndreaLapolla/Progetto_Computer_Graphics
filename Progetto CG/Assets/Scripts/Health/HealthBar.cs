using TMPro;
using UnityEngine;
using UnityEngine.UI;

// classe per gestire lo stato della barra di salute
public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject warrior;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject ronin;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private GameObject textValue;

    private Health playerHealth;

    private void Start()
    {
        SetFollowedCharacter();
        totalHealthBar.fillAmount = playerHealth.currentHealth / playerHealth.GetStartingHealth();
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / playerHealth.GetStartingHealth();
        SetTextValue();
    }

    // funzione per impostare sul testo il valore della salute attuale rispetto al totale 
    private void SetTextValue()
    {
        textValue.GetComponent<TextMeshProUGUI>().text = playerHealth.currentHealth + " / " + playerHealth.GetStartingHealth() + " HP";
    }
    
    private void SetFollowedCharacter()
    {
        if (warrior.activeSelf)
        {
            playerHealth = warrior.GetComponent<Health>();
            archer.SetActive(false);
            ronin.SetActive(false);
        }
        else if (ronin.activeSelf)
        {
            playerHealth = ronin.GetComponent<Health>();
            warrior.SetActive(false);
            archer.SetActive(false);
        }
        else if (archer.activeInHierarchy)
        {
            playerHealth = archer.GetComponent<Health>();
            warrior.SetActive(false);
            ronin.SetActive(false);
        }
    }
}
