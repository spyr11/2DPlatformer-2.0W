using UnityEngine;

public class DieState : IState
{
    private readonly BaseCharacter _character;
    private readonly Cooldown _cooldown;

    public DieState(BaseCharacter character)
    {
        _character = character;

        float dieTime = 1;

        _cooldown = new Cooldown(dieTime);
    }

    public void Enter()
    {
        _character.Rigidbody2D.gravityScale = _character.Rigidbody2D.gravityScale > 0 ? 0 : 1;
        _character.Rigidbody2D.velocity = Vector2.zero;

        if (_character.TryGetComponent(out Collider2D collider2D))
        {
            collider2D.enabled = false;
        }

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