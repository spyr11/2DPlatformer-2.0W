

using System;
using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    private readonly float _maxDamage = 10f;
    private readonly float _maxLifeTime = 2f;

    public event Action<Bullet> hit;

    private void OnEnable()
    {
        StartCoroutine(TryDisable(_maxLifeTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<IDamagable>(out IDamagable enemy) && IsDamageType(enemy))
        {
            enemy.TakeDamage(_maxDamage);
        }

        hit?.Invoke(this);
    }

    protected abstract bool IsDamageType(IDamagable character);

    private IEnumerator TryDisable(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        hit?.Invoke(this);
    }
}