using UnityEngine;

public class DieState : IState
{
    private readonly BaseCharacter _character;
    private readonly Cooldown _cooldown;
    private IDieView _animation;

    public DieState(BaseCharacter character, IDieView dieAnimation)
    {
        _character = character;

        float dieTime = 2;

        _cooldown = new Cooldown(dieTime);

        _animation = dieAnimation;
    }

    public void Enter()
    {
        if (_character.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.gravityScale = rigidbody2D.gravityScale > 0 ? 0 : 1;
        }

        if (_character.TryGetComponent<Collider2D>(out Collider2D collider2D))
        {
            collider2D.enabled = false;
        }

        _animation.StartDie();

        _cooldown.Start();
    }

    public void Exit() { }

    public void Update()
    {
        if (_cooldown.IsPassed)
        {
            _character.gameObject.SetActive(false);
        }
    }

    public void FixedUpdate() { }
}