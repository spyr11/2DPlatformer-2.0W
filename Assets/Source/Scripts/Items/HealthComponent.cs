using System;
using UnityEditor.Sprites;
using UnityEngine;

[RequireComponent(typeof(IDamagable))]
public class HealthComponent : MonoBehaviour
{
    [SerializeField, Range(0, 200)] private float _maxHealth;

    private IDamagable _damagable;
    private IHealable _healable;
    private float _currentHealth;

    public float CurrentValue => _currentHealth;

    public event Action<float> ValueChanged;

    private void Awake()
    {
        _damagable = GetComponent<IDamagable>();

        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        _damagable.Damaged += Decrease;

        if (TryGetComponent(out _healable))
        {
            _healable.Healed += Increase;
        }
    }

    private void OnDisable()
    {
        _damagable.Damaged -= Decrease;

        if (_healable != null)
        {
            _healable.Healed -= Increase;
        }
    }

    private void Increase(float value)
    {
        if (value <= 0)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth + value, 0, _maxHealth);

        ValueChanged?.Invoke(_currentHealth);
    }

    private void Decrease(float value)
    {
        if (value <= 0)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth - value, 0, _maxHealth);

        ValueChanged?.Invoke(_currentHealth);
    }
}
