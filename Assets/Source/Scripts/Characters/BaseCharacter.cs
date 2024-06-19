using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(HealthComponent))]
public abstract class BaseCharacter : MonoBehaviour, IDamagable
{
    [field: SerializeField] public BulletSpawner BulletSpawner { get; private set; }

    private HealthComponent _healthComponent;
    private Rigidbody2D _rigidbody2D;

    public abstract StateContext StateContext { get; }

    public HealthComponent HealthComponent => _healthComponent;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    public event Action<float> Damaged;

    private void Awake()
    {
        Init();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _healthComponent = GetComponent<HealthComponent>();
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