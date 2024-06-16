using System.Collections.Generic;

public class TopDownAttackState : CombatState
{
    public TopDownAttackState(Player player) : base(player) { }

    public override void Update()
    {
        base.Update();

        AttackEnemies(Player.EnemyChecker.Enemies);

        StateSwitcher.Switch<JumpState>();
    }

    private void AttackEnemies(List<IDamagable> enemies)
    {
        int count = enemies.Count;

        for (int i = 0; i < count; i++)
        {
            enemies[i].TakeDamage(Data.TopDownAttackValue);
        }
    }
}