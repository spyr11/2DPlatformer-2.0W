using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private readonly float _lifeTime = 2f;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDamage = 10f;

    private Rigidbody2D _rigidbody2D;
    private Coroutine _coroutine;
    private bool _isHit;

    public event Action<Bullet> Hit;
    public event Action<Bullet> Disabled;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _isHit = false;

        _coroutine = StartCoroutine(TryDisable());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isHit == false)
        {
            int bit = 1;

            if (collision.transform.TryGetComponent(out IDamagable character)
             && ((bit << collision.gameObject.layer) == _layerMask.value))
            {
                character.TakeDamage(_maxDamage);
            }

            Hit?.Invoke(this);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _isHit = true;
        }
    }


    private IEnumerator TryDisable()
    {
        yield return new WaitForSeconds(_lifeTime);

        Disabled?.Invoke(this);
    }
}