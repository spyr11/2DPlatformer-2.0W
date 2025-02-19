using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public abstract class BaseCharacter : MonoBehaviour, IDamagable
{
    [field: SerializeField] public BulletSpawner BulletSpawner { get; private set; }

    private Rigidbody2D _rigidbody2D;
    private Health _health;

    public event Action<float> Damaged;

    public abstract StateContext StateContext { get; }

    public float CurrentHealth => _health.CurrentValue;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _health = GetComponent<Health>();

        Init();
    }

    private void OnEnable()
    {
        OnEnabled();
    }

    private void OnDisable()
    {
        OnDisabled();

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

    public Vector3 GetPosition() => transform.position;

    protected abstract void Init();

    protected abstract void OnEnabled();

    protected abstract void OnDisabled();
}