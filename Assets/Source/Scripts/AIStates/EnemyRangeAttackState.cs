using UnityEngine;

public class EnemyRangeAttackState : EnemyState
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

        Vector2 direction = (player.GetRigidbody2D().transform.position - Enemy.transform.position).normalized;

        Attack(direction);

        Enemy.Animation.StartAttackRange();
    }

    public override void Update()
    {
        base.Update();

        if (Data.RangeCooldown.IsPassed)
        {
            if (MeleeAttack && MeleeAttack.HasEnemy)
            {
                StateSwitcher.Switch<EnemyMeleeAttackState>();
            }

            StateSwitcher.Switch<PatrolState>();
        }
    }

    private void Attack(Vector2 direction)
    {
        _bulletSpawner.SetDirection(direction);
    }
}


