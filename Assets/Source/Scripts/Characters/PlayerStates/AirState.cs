public class AirState : MovementState
{
    public AirState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();

        VelocityX = Data.SpeedOnAir;
    }
}