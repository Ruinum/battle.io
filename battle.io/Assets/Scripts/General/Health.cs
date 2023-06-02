using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;    

    public void DealDamage(float value)
    {
        _currentHealth -= value;
        ClampHealth();
    }

    public void Heal(float value)
    {
        _currentHealth += value;
        ClampHealth();
    }

    private void ClampHealth()
    {
        if (_currentHealth <= 0) _currentHealth = 0;
        if (_currentHealth >= _maxHealth) _currentHealth = _maxHealth;
    }
}
