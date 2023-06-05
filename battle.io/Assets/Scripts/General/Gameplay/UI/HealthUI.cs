using Ruinum.Utils;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    private Health _health;
    private float _maxFillAmount;

    private void Start()
    {
        _health = this.GetComponentInObject<Health>();
        
        _health.OnHealthChange += OnHealthChange;
        _health.OnMaxHealthChange += OnMaxHealthChange;
    }

    private void OnMaxHealthChange(float currentMaxHealth)
    {
        _maxFillAmount = currentMaxHealth;       
    }

    private void OnHealthChange(float currentHealth)
    {
        _image.fillAmount = currentHealth / _maxFillAmount;
    }
}