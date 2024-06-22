using UnityEngine;

public class MovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly PlayerData Data;
    protected readonly Player Player;

    private readonly Quaternion _turnRight = Quaternion.Euler(0, 0, 0);
    private readonly Quaternion _turnLeft = Quaternion.Euler(0, 180, 0);

    protected float VelocityX { get; set; }

    public MovementState(Player player)
    {
        StateSwitcher = player.StateContext;
        Data = player.Data;
        Player = player;
    }

    public virtual void Enter()
    {
        Player.Damaged += OnDamaged;
    }

    public virtual void Exit()
    {
        Player.Damaged -= OnDamaged;
    }

    public virtual void Update()
    {
        if (Player.HealthComponent.CurrentValue <= 0)
        {
            Player.Animation.StartDie();

            StateSwitcher.Switch<DieState>();
        }

        if (Player.AttackPressed && Data.RangeCooldown.IsPassed)
        {
            StateSwitcher.Switch<RangeAttackState>();
        }

        SetStateOnPlatform();
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Player.ReadHorizontal();

        Player.Rigidbody2D.transform.rotation = GetDirection(horizontalInput);

        float deltaX = VelocityX * horizontalInput;
        float deltaY = Player.Rigidbody2D.velocity.y;

        Vector2 movement = new Vector2(deltaX, deltaY);
        Player.Rigidbody2D.velocity = movement;
    }

    private void OnDamaged(float damage)
    {
        StateSwitcher.Switch<DamagedState>();
    }

    private void SetStateOnPlatform()
    {
        if (Player.GroundChecker.IsGrounded && Player.GroundChecker.Platform != null)
        {
            Player.gameObject.transform.SetParent(Player.GroundChecker.Platform.transform);
        }
        else
        {
            Player.gameObject.transform.SetParent(null);
        }
    }

    private Quaternion GetDirection(float value)
    {
        if (value > 0)
        {
            return _turnRight;
        }
        else if (value < 0)
        {
            return _turnLeft;
        }

        return Player.Rigidbody2D.transform.rotation;
    }
}