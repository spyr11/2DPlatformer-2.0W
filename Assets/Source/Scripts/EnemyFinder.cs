using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    private readonly List<IDamagable> _enemies = new List<IDamagable>();

    private int _maxCapacity = 10;

    public List<IDamagable> Enemies => _enemies;
    public bool HasEnemy => _enemies.Count > 0;

    private void Awake()
    {
        _enemies.Capacity = _maxCapacity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable enemy) && enemy is Enemy)
        {
            _enemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable enemy) && enemy is Enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}