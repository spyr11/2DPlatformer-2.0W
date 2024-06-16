using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthComponent))]
public abstract class BaseCharacter : MonoBehaviour, IDamagable
{
    [SerializeField] private BulletSpawner _bulletSpawner;

    private Rigidbody2D _rigidbody2D;
    private HealthComponent _health;
    private bool _isAlive;

    public BulletSpawner BulletSpawner => _bulletSpawner;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public HealthComponent Health => _health;
    public bool IsAlive => _isAlive;

    public abstract StateContext StateContext { get; }

    public event Action<float> Damaged;

    private void Awake()
    {
        Init();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _health = GetComponent<HealthComponent>();

        _isAlive = true;
    }

    private void OnEnable()
    {
        OnEnabled();

        Health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        OnDisabled();

        Health.HealthChanged -= OnHealthChanged;

        Destroy(gameObject);
    }

    private void Update()
    {
        StateContext.Update();
    }

    private void FixedUpdate()
    {
        StateContext.FixedUpdate();
    }

    public void TakeDamage(float damage) => Damaged?.Invoke(damage);

    public Rigidbody2D GetRigidbody2D() => _rigidbody2D;

    protected abstract void Init();

    protected abstract void OnEnabled();

    protected abstract void OnDisabled();

    private void OnHealthChanged(float health)
    {
        if (health <= 0f)
        {
            _isAlive = false;
        }
    }
}