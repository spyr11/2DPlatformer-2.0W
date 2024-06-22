using System;
using UnityEngine;

[RequireComponent(typeof(IDamagable))]
public class Health : MonoBehaviour, IIndicator
{
    [SerializeField, Range(0, 200)] private float _maxHealth;

    private IDamagable _damagable;
    private IHealable _healable;
    private float _currentHealth;

    public event Action<float> Changed;

    public float CurrentValue => _currentHealth;
    public float MaxValue => _maxHealth;

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

        Changed?.Invoke(_maxHealth);
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

        Changed?.Invoke(_currentHealth);
    }

    private void Decrease(float value)
    {
        if (value <= 0)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth - value, 0, _maxHealth);

        Changed?.Invoke(_currentHealth);
    }
}
