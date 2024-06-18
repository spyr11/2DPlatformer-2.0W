public class WalkState : GroundState
{
    public WalkState(Player character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        VelocityX = Data.Speed;

        Player.Animation.StartWalking();
    }

    public override void Exit()
    {
        base.Exit();

        Player.Animation.StopWalking();
    }

    public override void Update()
    {
        base.Update();

        if (Player.ReadHorizontal() == 0)
        {
            StateSwitcher.Switch<IdleState>();
        }
    }
}