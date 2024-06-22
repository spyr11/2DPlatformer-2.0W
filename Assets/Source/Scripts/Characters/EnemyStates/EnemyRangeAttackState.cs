using UnityEngine;

public class EnemyRangeAttackState : EnemyMovementState
{
    private readonly BulletSpawner _bulletSpawner;

    public EnemyRangeAttackState(Enemy enemy) : base(enemy)
    {
        _bulletSpawner = enemy.BulletSpawner;
    }

    public override void Enter()
    {
        base.Enter();

        Data.RangeCooldown.Start();

        IDamagable player = RangeAttack.Player;

        Vector2 direction = (player.GetPosition() - Enemy.transform.position).normalized;

        Attack(direction);
    }

    public override void Update()
    {
        base.Update();

        if (Data.RangeCooldown.IsPassed)
        {
            if (MeleeAttack && MeleeAttack.IsDetected)
            {
                StateSwitcher.Switch<EnemyMeleeAttackState>();
            }

            if (Enemy.PlayerFinder.IsDetected)
            {
                StateSwitcher.Switch<ChaseState>();

            }
            else
            {
                StateSwitcher.Switch<PatrolState>();
            }
        }
    }

    private void Attack(Vector2 direction)
    {
        _bulletSpawner.SetDirection(direction);
    }
}