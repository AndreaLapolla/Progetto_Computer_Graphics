using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// todo rivedere per le altre classi
// classe per gestire lo stato della barra di salute
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / playerHealth.GetStartingHealth();
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / playerHealth.GetStartingHealth();
    }
}
