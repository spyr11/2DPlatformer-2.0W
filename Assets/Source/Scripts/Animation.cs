using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Movement))]
public class Animation : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        _movement.OnJumpingChanged += Jump;
        _movement.OnSpeedChanged += Move;
    }

    private void OnDisable()
    {
        _movement.OnJumpingChanged -= Jump;
        _movement.OnSpeedChanged -= Move;
    }

    private void Move(float speed)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(speed));
    }

    private void Jump(bool isJumping)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsJumping, isJumping);
    }
}
