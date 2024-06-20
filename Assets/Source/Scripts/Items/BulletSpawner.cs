using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shootForce;

    private ObjectPool<Bullet> _pool;
    private Vector2 _direction;

    private int _poolMaxCapacity = 20;
    private int _poolMaxSize = 20;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
                createFunc: CreateObject,
                actionOnGet: SetObjectState,
                actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
                actionOnDestroy: DestroyObject,
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
        bullet.Hit += ReleaseObject;
        bullet.Disabled += ReleaseObject;

        return bullet;
    }

    private void DestroyObject(Bullet bullet)
    {
        bullet.Hit -= ReleaseObject;
        bullet.Disabled -= ReleaseObject;

        Destroy(bullet.gameObject);
    }

    private void SetObjectState(Bullet bullet)
    {
        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
        bullet.Rigidbody2D.velocity = _direction * _shootForce;
    }

    private void Shoot()
    {
        _pool.Get();
    }

    private void ReleaseObject(Bullet bullet)
    {
        _pool.Release(bullet);
    }
}