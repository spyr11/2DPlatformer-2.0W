using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
        public static readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));
        public static readonly int IsHealing = Animator.StringToHash(nameof(IsHealing));
        public static readonly int IsDying = Animator.StringToHash(nameof(IsDying));
        public static readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    }
}
