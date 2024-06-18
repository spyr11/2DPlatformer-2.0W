public class FallState : AirState
{
    public FallState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();

        Player.Animation.StartFalling();
    }

    public override void Exit()
    {
        base.Exit();

        Player.Animation.StopFalling();
    }

    public override void Update()
    {
        base.Update();

        if (Player.GroundChecker.IsGrounded == false && Player.EnemyChecker.HasEnemy)
        {
            StateSwitcher.Switch<TopDownAttackState>();
        }
        else if (Player.GroundChecker.IsGrounded)
        {
            StateSwitcher.Switch<IdleState>();
        }
    }
}