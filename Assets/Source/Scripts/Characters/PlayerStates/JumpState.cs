using UnityEngine;

public class JumpState : AirState
{
    public JumpState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();

        CanJump = true;

        Player.Animation.Jump();
    }

    public override void Update()
    {
        base.Update();

        if (Player.Rigidbody2D.velocity.y < 0 && CanJump == false)
        {
            StateSwitcher.Switch<FallState>();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (CanJump)
        {
            Jump();

            CanJump = false;
        }
    }

    private void Jump()
    {
        Player.Rigidbody2D.AddForce(Data.JumpForce * Vector2.up, ForceMode2D.Impulse);
    }
}