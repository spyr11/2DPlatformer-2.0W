using UnityEngine;

public class ChaseState : EnemyMovementState
{
    private readonly float _multiplier;

    private IDamagable _player;

    private float _speedX;
    private float _speedY;

    public ChaseState(Enemy enemy) : base(enemy)
    {
        _multiplier = 1.5f;
    }

    public override void Enter()
    {
        base.Enter();

        _speedX = Data.Speed * _multiplier;
        _speedY = Data.Speed * _multiplier;

        _player = Enemy.PlayerFinder.Player;

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

        if (MeleeAttack && MeleeAttack.IsDetected && Data.MeleeCooldown.IsPassed)
        {
            StateSwitcher.Switch<EnemyMeleeAttackState>();
        }

        if (RangeAttack && RangeAttack.IsDetected && Data.RangeCooldown.IsPassed)
        {
            StateSwitcher.Switch<EnemyRangeAttackState>();
        }

        if (Enemy.PlayerFinder.IsDetected)
        {
            Vector2 direction = (_player.GetPosition() - Enemy.Rigidbody2D.transform.position);

            SetDirectionAndSpeed(direction, _speedX, _speedY);
        }
        else
        {
            StateSwitcher.Switch<PatrolState>();
        }
    }
}


