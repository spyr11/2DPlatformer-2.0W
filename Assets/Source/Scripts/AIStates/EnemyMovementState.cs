using UnityEngine;

public class EnemyMovementState : IState
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

        float deltaY = Enemy.Data.CanFly ? VelocityY : Enemy.Rigidbody2D.velocity.y;

        Enemy.Rigidbody2D.velocity = new Vector2(VelocityX, deltaY);
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


