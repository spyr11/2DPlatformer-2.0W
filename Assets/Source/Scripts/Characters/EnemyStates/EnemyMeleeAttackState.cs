using Unity.VisualScripting;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyMovementState
{
    public EnemyMeleeAttackState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();

        Data.MeleeCooldown.Start();

        IDamagable player = Enemy.PlayerChecker.Player;

        player.TakeDamage(Data.MeleeAttackValue);

        Enemy.Animation.StartAttackMelee();
    }

    public override void Update()
    {
        base.Update();

        if (Data.MeleeCooldown.IsPassed)
        {
            StateSwitcher.Switch<PatrolState>();
        }
    }
}
