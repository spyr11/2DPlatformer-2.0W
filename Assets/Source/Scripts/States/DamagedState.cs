using UnityEngine;

public class DamagedState : MovementState
{
    private readonly float _damageDelay;

    private float _timer;

    public DamagedState(Player player) : base(player)
    {
        _damageDelay = 0.2f;
    }

    public override void Enter()
    {
        base.Enter();

        _timer = 0f;

        Player.Animation.TakeDamage();
    }

    public override void Update()
    {
        base.Update();

        if (CanSwitsh())
        {
            StateSwitcher.Switch<IdleState>();
        }
    }

    private bool CanSwitsh()
    {
        _timer += Time.deltaTime;

        return _timer >= _damageDelay;
    }
}