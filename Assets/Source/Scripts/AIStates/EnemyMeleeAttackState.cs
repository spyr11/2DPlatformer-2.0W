using UnityEngine;

public class EnemyMeleeAttackState : EnemyState
{
    private IDamagable _player;

    public EnemyMeleeAttackState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();

        Data.MeleeCooldown.Start();

        _player = Enemy.PlayerChecker.Player;
        _player.TakeDamage(Data.MeleeAttackValue);

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

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _player.GetRigidbody2D().AddForce(MeleeAttack.transform.right * Data.MeleeAttackForce, ForceMode2D.Force);

    }
}
