using UnityEngine;

public class EnemyState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly EnemyData Data;
    protected readonly Enemy Enemy;

    private readonly Quaternion _turnRight = Quaternion.Euler(0, 0, 0);
    private readonly Quaternion _turnLeft = Quaternion.Euler(0, 180, 0);

    protected float VelocityX { get; set; }
    protected float VelocityY { get; set; }

    protected PlayerChecker MeleeAttack { get; private set; }
    protected PlayerChecker RangeAttack { get; private set; }

    public EnemyState(Enemy enemy)
    {
        Enemy = enemy;
        Data = enemy.Data;
        StateSwitcher = enemy.StateContext;
        MeleeAttack = enemy.MeleeAttack;
        RangeAttack = enemy.RangeAttack;
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
        if (Enemy.IsAlive == false)
        {
            StateSwitcher.Switch<DieState>();
        }
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    private void TakeDamage(float damage)
    {
        StateSwitcher.Switch<EnemyDamagedState>();
    }

    private void Move()
    {
        Enemy.Rigidbody2D.transform.rotation = Direction();

        float deltaX = VelocityX;
        float deltaY = Enemy.Rigidbody2D.gravityScale == 0 ? VelocityY : Enemy.Rigidbody2D.velocity.y;

        Enemy.Rigidbody2D.velocity = new Vector2(deltaX, deltaY);
    }

    private Quaternion Direction()
    {
        if (VelocityX > 0)
        {
            return _turnRight;
        }
        else if (VelocityX < 0)
        {
            return _turnLeft;
        }

        return Enemy.Rigidbody2D.transform.rotation;
    }
}


