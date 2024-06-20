using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDamage = 10f;

    private Rigidbody2D _rigidbody2D;
    private bool _isHit;

    public event Action<Bullet> Hit;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _isHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isHit == false)
        {
            if (collision.transform.TryGetComponent(out IDamagable character)
             && ((1 << collision.gameObject.layer) == _layerMask.value))
            {
                character.TakeDamage(_maxDamage);
            }

            Hit?.Invoke(this);

            _isHit = true;
        }
    }
}