using System;

public class EnemyBullet : Bullet
{
    protected override bool IsDamageType(IDamagable character)
    {
        return character is Player;
    }
}
