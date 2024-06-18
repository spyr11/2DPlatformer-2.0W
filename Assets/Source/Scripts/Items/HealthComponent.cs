using System;
using UnityEngine;

[RequireComponent(typeof(IDamagable))]
public class HealthComponent : MonoBehaviour, IChangeable
{
    [SerializeField, Range(0, 200)] private float _maxHealth;

    private IDamagable _character;
    private float _currentHealth;

    public float CurrentValue => _currentHealth;

    public event Action<float> ValueChanged;

    private void Awake()
    {
        _character = GetComponent<IDamagable>();
        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        _character.Damaged += Decrease;
    }

    private void OnDisable()
    {
        _character.Damaged -= Decrease;
    }

    private void Start()
    {
    }

    public void OnPicked(float value)
    {
        Increase(value);
    }

    private void Increase(float value)
    {
        _currentHealth += value;

        ValueChanged?.Invoke(_currentHealth);
    }

    private void Decrease(float value)
    {
        _currentHealth -= value;

        ValueChanged?.Invoke(_currentHealth);
    }
}
