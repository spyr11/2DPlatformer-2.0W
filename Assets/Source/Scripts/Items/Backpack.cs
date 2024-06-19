using System;
using UnityEngine;

[RequireComponent(typeof(Picker))]
public class Backpack : MonoBehaviour
{
    private Picker _picker;

    private float _coinsCount = 0;

    public event Action<float> ValueChanged;

    private void Awake()
    {
        _picker = GetComponent<Picker>();
    }

    private void OnEnable()
    {
        _picker.CoinPicked += OnPicked;
    }

    private void OnDisable()
    {
        _picker.CoinPicked -= OnPicked;
    }

    private void OnPicked(float value)
    {
        _coinsCount += value;

        ValueChanged?.Invoke(_coinsCount);
    }
}