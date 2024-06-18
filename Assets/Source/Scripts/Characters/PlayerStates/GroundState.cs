public class GroundState : MovementState
{
    public GroundState(Player player) : base(player) { }

    public override void Update()
    {
        base.Update();

        if (Player.JumpPressed)
        {
            StateSwitcher.Switch<JumpState>();
        }

        if (Player.GroundChecker.IsGrounded == false)
        {
            StateSwitcher.Switch<FallState>();
        }
    }
}