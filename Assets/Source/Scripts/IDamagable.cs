using System;
using UnityEngine;

public interface IDamagable
{
    event Action<float> Damaged;

    void TakeDamage(float damage);

    Rigidbody2D GetRigidbody2D();
}
