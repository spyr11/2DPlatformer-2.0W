using System;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override bool IsDamageType(IDamagable character)
    {
        return character is Enemy;
    }
}