using System;

public class EnemyBullet : Bullet
{
    protected override bool HasType(IDamagable character)
    {
        return character is Player;
    }
}
