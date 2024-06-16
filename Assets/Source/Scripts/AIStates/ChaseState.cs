using UnityEngine;

public class ChaseState : EnemyMovementState
{
    private IDamagable _player;

    private readonly float _multiplier;

    public ChaseState(Enemy enemy) : base(enemy)
    {
        _multiplier = 1.5f;
    }

    public override void Enter()
    {
        base.Enter();

        _player = Enemy.PlayerChecker.Player;

        Enemy.Animation.StartChasing();
    }

    public override void Exit()
    {
        base.Exit();

        Enemy.Animation.StopChasing();
    }

    public override void Update()
    {
        base.Update();

        if (MeleeAttack && MeleeAttack.HasEnemy && Data.MeleeCooldown.IsPassed)
        {
            StateSwitcher.Switch<EnemyMeleeAttackState>();
        }

        if (RangeAttack && RangeAttack.HasEnemy && Data.RangeCooldown.IsPassed)
        {
            StateSwitcher.Switch<EnemyRangeAttackState>();
        }

        if (Enemy.PlayerChecker.HasEnemy)
        {
            Vector2 direction = (_player.GetRigidbody2D().transform.position - Enemy.Rigidbody2D.transform.position).normalized;

            VelocityX = Data.Speed * direction.x * _multiplier;
            VelocityY = Data.Speed * direction.y * _multiplier;
        }
        else
        {
            StateSwitcher.Switch<PatrolState>();
        }
    }
}


