using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour, IDieView
{
    private Animator _animator;

    public void Init() => _animator = GetComponent<Animator>();

    public void StartWalking() => _animator.SetBool(PlayerAnimatorData.Params.IsWalking, true);
    public void StopWalking() => _animator.SetBool(PlayerAnimatorData.Params.IsWalking, false);

    public void StartFalling() => _animator.SetBool(PlayerAnimatorData.Params.IsFalling, true);
    public void StopFalling() => _animator.SetBool(PlayerAnimatorData.Params.IsFalling, false);

    public void TakeDamage() => _animator.SetTrigger(PlayerAnimatorData.Params.TakeDamage);

    public void Attack() => _animator.SetTrigger(PlayerAnimatorData.Params.Attack);

    public void Jump() => _animator.SetTrigger(PlayerAnimatorData.Params.Jump);

    public void StartDie() => _animator.SetTrigger(PlayerAnimatorData.Params.Die);
}
