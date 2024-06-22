using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VampireState : MovementState
{
    public VampireState(Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();

        Data.Vampirism.Start();

        VelocityX = Data.Speed;

        Player.Animation.StartHealing();
    }

    public override void Exit()
    {
        base.Exit();

        Player.Animation.StopHealing();
    }
    public override void Update()
    {
        base.Update();

        if (Player.VampireVictimFinder.HasEnemy)
        {
            StealHealth();
        }
        else
        {
            Player.VampirismLightning.Disable();
        }


        if (Data.Vampirism.IsPassed)
        {
            StateSwitcher.Switch<IdleState>();
        }
    }

    private IDamagable GetNearestEnemy(IReadOnlyList<IDamagable> enemies)
    {
        return enemies.OrderBy(enemy => Vector3.SqrMagnitude(Player.transform.position - enemy.GetPosition())).First();
    }

    private void StealHealth()
    {
        IDamagable enemy = GetNearestEnemy(Player.VampireVictimFinder.Enemies);

        float stolenValue = Data.vampirismValue * Time.deltaTime;
        stolenValue = stolenValue < enemy.CurrentHealth ? stolenValue : enemy.CurrentHealth;

        Player.Heal(stolenValue);

        enemy.TakeDamage(stolenValue);

        Player.VampirismLightning.SetDirection(Player.transform.position, enemy.GetPosition());
    }
}