using TMPro;
using UnityEngine;
using UnityEngine.UI;

// classe per gestire lo stato della barra di salute
public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject characterSelector;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private GameObject textValue;

    private Health playerHealth;

    private void Start()
    {
        playerHealth = characterSelector.GetComponent<CharacterSelector>().GetPlayedCharacter().GetComponent<Health>();
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
        textValue.GetComponent<TextMeshProUGUI>().text = playerHealth.currentHealth + " / " + 
                                                         playerHealth.GetStartingHealth() + " HP";
    }
}
