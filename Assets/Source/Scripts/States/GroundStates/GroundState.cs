public class GroundState : MovementState
{
    public GroundState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Player.JumpPressed)
        {
            StateSwitcher.Switch<JumpState>();
        }

        if (Player.GroundChecker.IsGrounded == false && CanJump == false)
        {
            StateSwitcher.Switch<FallState>();
        }

        SetStateOnPlatform();
    }

    private void SetStateOnPlatform()
    {
        if (Player.GroundChecker.IsGrounded && Player.GroundChecker.Box != null)
        {
            Player.gameObject.transform.SetParent(Player.GroundChecker.Box.transform);
        }
        else
        {
            Player.gameObject.transform.SetParent(null);
        }
    }
}