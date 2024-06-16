public class AirState : MovementState
{
    public AirState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();

        float airDivisor = 2f;

        VelocityX = Data.Speed / airDivisor;
    }
}