using System;
using UnityEngine;

public class Backpack : MonoBehaviour, IChangeable
{
    private float _coinsCount = 0;

    public event Action<float> ValueChanged;

    public void OnPicked(float value)
    {
        _coinsCount += value;

        ValueChanged?.Invoke(_coinsCount);
    }
}