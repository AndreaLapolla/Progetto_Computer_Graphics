using TMPro;
using UnityEngine;
using UnityEngine.UI;

// classe per gestire lo stato della barra di salute
public class HealthBar : MonoBehaviour
{
    [Header("Two options")]
    [SerializeField] private GameObject characterSelector;
    [SerializeField] private GameObject boss;
    
    [Header("Health Bar Components")]
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private GameObject textValue;

    private Health _health;

    private void Start()
    {
        if (characterSelector != null)
        {
            _health = characterSelector.GetComponent<CharacterSelector>().GetPlayedCharacter().GetComponent<Health>();
        }
        else if (boss != null)
        {
            _health = boss.GetComponent<Health>();
        }
        
        totalHealthBar.fillAmount = _health.CurrentHealth / _health.GetStartingHealth();
    }

    private void Update()
    {
        currentHealthBar.fillAmount = _health.CurrentHealth / _health.GetStartingHealth();
        SetTextValue();
    }

    // funzione per impostare sul testo il valore della salute attuale rispetto al totale 
    private void SetTextValue()
    {
        textValue.GetComponent<TextMeshProUGUI>().text = _health.CurrentHealth + " / " + 
                                                         _health.GetStartingHealth() + " HP";
    }
}
