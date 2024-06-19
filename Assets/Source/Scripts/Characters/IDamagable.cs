using System;
using UnityEngine;

public interface IDamagable
{
    event Action<float> Damaged;

    void TakeDamage(float damage);

    Vector3 GetPosition();
}