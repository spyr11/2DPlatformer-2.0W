public class RangeAttackState : CombatState
{
    private readonly BulletSpawner _bulletSpawner;

    public RangeAttackState(Player player) : base(player)
    {
        _bulletSpawner = player.BulletSpawner;
    }

    public override void Enter()
    {
        base.Enter();

        VelocityX = 0f;

        Data.RangeCooldown.Start();

        Player.Animation.Attack();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        _bulletSpawner.SetDirection(Player.transform.right);

        StateSwitcher.Switch<IdleState>();
    }
}
