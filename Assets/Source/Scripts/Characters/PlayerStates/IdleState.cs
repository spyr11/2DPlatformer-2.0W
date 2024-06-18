public class IdleState : GroundState
{
    public IdleState(Player player) : base(player) { }

    public override void Update()
    {
        base.Update();

        if (Player.ReadHorizontal() != 0)
        {
            StateSwitcher.Switch<WalkState>();
        }
    }
}