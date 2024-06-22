using System;
using UnityEngine;

public interface IDamagable
{
    event Action<float> Damaged;

    float CurrentHealth { get; }

    void TakeDamage(float damage);

    Vector3 GetPosition();
}