using UnityEngine;

public class EnemyView : MonoBehaviour, IDieView
{
    public const string IsChasing = nameof(IsChasing);
    public const string TakeDamage = nameof(TakeDamage);
    public const string Die = nameof(Die);
    public const string AttackMelee = nameof(AttackMelee);
    public const string AttackRange = nameof(AttackRange);
    public const string IsPatrolling = nameof(IsPatrolling);

    private Animator _animator;
    public void Init() => _animator = GetComponent<Animator>();

    public void StartPatrolling() => _animator.SetBool(IsPatrolling, true);
    public void StopPatrolling() => _animator.SetBool(IsPatrolling, false);

    public void StartChasing() => _animator.SetBool(IsChasing, true);
    public void StopChasing() => _animator.SetBool(IsChasing, false);

    public void StartAttackMelee() => _animator.SetTrigger(AttackMelee);
    public void StartAttackRange() => _animator.SetTrigger(AttackRange);
    public void StartTakeDamage() => _animator.SetTrigger(TakeDamage);
    public void StartDie() => _animator.SetTrigger(Die);
}
