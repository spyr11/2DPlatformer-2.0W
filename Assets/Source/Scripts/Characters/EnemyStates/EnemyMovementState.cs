using UnityEngine;

public class EnemyMovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly EnemyData Data;
    protected readonly Enemy Enemy;

    private readonly Quaternion _turnRight = Quaternion.Euler(0, 0, 0);
    private readonly Quaternion _turnLeft = Quaternion.Euler(0, 180, 0);

    private float _velocityX;
    private float _velocityY;

    protected PlayerFinder MeleeAttack { get; private set; }
    protected PlayerFinder RangeAttack { get; private set; }

    public EnemyMovementState(Enemy enemy)
    {
        Enemy = enemy;
        Data = Enemy.Data;
        StateSwitcher = Enemy.StateContext;
        MeleeAttack = Enemy.MeleeAttack;
        RangeAttack = Enemy.RangeAttack;

        float gravityScale = Enemy.Data.CanFly ? 0 : Enemy.Rigidbody2D.gravityScale;

        Enemy.Rigidbody2D.gravityScale = gravityScale;
    }

    public virtual void Enter()
    {
        Enemy.Damaged += TakeDamage;
    }

    public virtual void Exit()
    {
        Enemy.Damaged -= TakeDamage;
    }

    public virtual void Update()
    {
        if (Enemy.CurrentHealth <= 0)
        {
            Enemy.Animation.StartDie();

            StateSwitcher.Switch<DieState>();
        }
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    protected void SetDirectionAndSpeed(Vector3 direction, float velocityX, float velocityY)
    {
        if (Enemy.Data.CanFly)
        {
            direction.Normalize();

            _velocityX = velocityX * direction.x;
            _velocityY = velocityY * direction.y;
        }
        else
        {
            _velocityX = velocityX * Mathf.Sign(direction.x);
        }
    }

    private void TakeDamage(float damage)
    {
        StateSwitcher.Switch<EnemyDamagedState>();
    }

    private void Move()
    {
        Enemy.Rigidbody2D.transform.rotation = GetBodyDirection();

        float deltaY = Enemy.Data.CanFly ? _velocityY : Enemy.Rigidbody2D.velocity.y;

        Enemy.Rigidbody2D.velocity = new Vector2(_velocityX, deltaY);
    }

    private Quaternion GetBodyDirection()
    {
        if (_velocityX > 0)
        {
            return _turnRight;
        }
        else if (_velocityX < 0)
        {
            return _turnLeft;
        }

        return Enemy.Rigidbody2D.transform.rotation;
    }
}