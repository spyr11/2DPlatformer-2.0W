using System;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override bool HasType(IDamagable character)
    {
        return character is Enemy;
    }
}