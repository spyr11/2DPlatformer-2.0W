using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _lifeTime;

    private ObjectPool<Bullet> _pool;

    private Vector2 _direction;

    private int _poolCapacity = 10;
    private int _poolMaxSize = 10;

    private float _shootDamage;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
                createFunc: () => Instantiate(_bulletPrefab),
                actionOnGet: (bullet) => ActionOnGet(bullet),
                actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
                actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);

        _direction = transform.right;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;

        Shoot();
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.hit += OnHit;

        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().velocity = _direction * _shootForce;
    }

    private void Shoot()
    {
        _pool.Get();
    }

    private void OnHit(Bullet bullet)
    {
        bullet.hit -= OnHit;

        _pool.Release(bullet);

    }
}

