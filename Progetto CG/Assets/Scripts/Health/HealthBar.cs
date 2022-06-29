using TMPro;
using UnityEngine;
using UnityEngine.UI;

// classe per gestire lo stato della barra di salute
public class HealthBar : MonoBehaviour
{
    [Header("Character Selector")]
    [SerializeField] private GameObject characterSelector;
    
    [Header("Health Bar Components")]
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private GameObject textValue;

    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = characterSelector.GetComponent<CharacterSelector>().GetPlayedCharacter().GetComponent<Health>();
        totalHealthBar.fillAmount = _playerHealth.CurrentHealth / _playerHealth.GetStartingHealth();
    }

    private void Update()
    {
        currentHealthBar.fillAmount = _playerHealth.CurrentHealth / _playerHealth.GetStartingHealth();
        SetTextValue();
    }

    // funzione per impostare sul testo il valore della salute attuale rispetto al totale 
    private void SetTextValue()
    {
        textValue.GetComponent<TextMeshProUGUI>().text = _playerHealth.CurrentHealth + " / " + 
                                                         _playerHealth.GetStartingHealth() + " HP";
    }
}
