using UnityEngine;

public class EnemyDamagedState : EnemyMovementState
{
    private readonly float _damageDelay;

    private float _timer;

    public EnemyDamagedState(Enemy enemy) : base(enemy)
    {
        _damageDelay = 1f;
    }

    public override void Enter()
    {
        base.Enter();

        _timer = 0f;

        Enemy.Animation.StartTakeDamage();
    }

    public override void Update()
    {
        base.Update();

        if (CanSwitsh())
        {
            StateSwitcher.Switch<PatrolState>();
        }
    }

    private bool CanSwitsh()
    {
        _timer += Time.deltaTime;

        return _timer >= _damageDelay;
    }
}


