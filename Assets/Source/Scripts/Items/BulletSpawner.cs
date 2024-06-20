using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _lifeTime;

    private Dictionary<Bullet, Coroutine> _coroutines;
    private ObjectPool<Bullet> _pool;
    private Vector2 _direction;

    private int _poolMaxCapacity = 20;
    private int _poolMaxSize = 20;

    private void Awake()
    {
        _coroutines = new Dictionary<Bullet, Coroutine>();

        _pool = new ObjectPool<Bullet>(
                createFunc: () => CreateObject(),
                actionOnGet: (bullet) => SetObjectState(bullet),
                actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
                actionOnDestroy: (bullet) => DestroyObject(bullet),
                collectionCheck: true,
                defaultCapacity: _poolMaxCapacity,
                maxSize: _poolMaxSize);

        _direction = transform.right;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;

        Shoot();
    }

    private Bullet CreateObject()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.Hit += OnHit;

        Coroutine coroutine = null;
        _coroutines.Add(bullet, coroutine);

        return bullet;
    }

    private void DestroyObject(Bullet bullet)
    {
        bullet.Hit -= OnHit;

        _coroutines.Remove(bullet);

        Destroy(bullet.gameObject);
    }

    private void SetObjectState(Bullet bullet)
    {
        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
        bullet.Rigidbody2D.velocity = _direction * _shootForce;

        _coroutines[bullet] = StartCoroutine(TryRelease(bullet));
    }

    private void Shoot()
    {
        _pool.Get();
    }

    private void OnHit(Bullet bullet)
    {
        _pool.Release(bullet);

        if (_coroutines[bullet] != null)
        {
            StopCoroutine(_coroutines[bullet]);
        }
    }

    private IEnumerator TryRelease(Bullet bullet)
    {
        yield return new WaitForSeconds(_lifeTime);

        _pool.Release(bullet);
    }
}