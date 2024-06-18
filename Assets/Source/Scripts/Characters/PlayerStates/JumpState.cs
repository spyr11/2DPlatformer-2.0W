using UnityEngine;

public class JumpState : AirState
{
    private bool _canJump;

    public JumpState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();

        _canJump = true;

        Player.Animation.Jump();
    }

    public override void Update()
    {
        base.Update();

        if (Player.Rigidbody2D.velocity.y < 0 && _canJump == false)
        {
            StateSwitcher.Switch<FallState>();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_canJump)
        {
            Jump();

            _canJump = false;
        }
    }

    private void Jump()
    {
        Player.Rigidbody2D.AddForce(Data.JumpForce * Vector2.up, ForceMode2D.Impulse);
    }
}