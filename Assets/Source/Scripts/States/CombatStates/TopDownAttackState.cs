using System.Collections.Generic;

public class TopDownAttackState : CombatState
{
    public TopDownAttackState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        List<IDamagable> enemies = Player.EnemyChecker.Enemies;

        int count = enemies.Count;

        for (int i = 0; i < count; i++)
        {
            enemies[i].TakeDamage(Data.TopDownAttackValue);
        }

        StateSwitcher.Switch<JumpState>();

    }
}