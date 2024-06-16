using System;
using UnityEngine;

[RequireComponent(typeof(IDamagable))]
public class HealthComponent : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0, 200)] private float _maxHealth;

    private IDamagable _character;

    private float _currentHealth;

    public event Action<float> HealthChanged;

    private void Awake()
    {
        _character = GetComponent<IDamagable>();
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
        _currentHealth = _maxHealth;
    }

    public void Use(float value)
    {
        Increase(value);
    }

    private void Increase(float value)
    {
        _currentHealth += value;

        HealthChanged?.Invoke(_currentHealth);
    }

    private void Decrease(float value)
    {
        _currentHealth -= value;

        HealthChanged?.Invoke(_currentHealth);
    }
}
