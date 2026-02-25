using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Image healthBar; // Drag your UI Image here
    [SerializeField] private Image radiationBar; // Drag your UI Image here
    [SerializeField] private int baseMaxHealth = 100; // Base maximum health
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int radiationAmount = 0; // Current radiation amount
    
    private int maxHealth;
    private int _radiationAmount;

    public int RadiationAmount
    {
        get => _radiationAmount;
        set
        {
            _radiationAmount = value;
            CalculateMaxHealth();
        }
    }

    void Start()
    {
        maxHealth = baseMaxHealth;
        UpdateUI();
    }

    private void CalculateMaxHealth()
    {
        maxHealth = baseMaxHealth - _radiationAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        radiationBar.fillAmount = (float)_radiationAmount / baseMaxHealth;
    }

    private void Update()
    {
        RadiationAmount = radiationAmount;
    }
    
    public void TakeRadiation(int amount)
    {
        radiationAmount += amount;
        CalculateMaxHealth();
    }
}