using System;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField, Range(0, 10)] public float MeleeAttackValue { get; private set; }
    [field: SerializeField, Range(0, 10)] public float RangeAttackValue { get; private set; }
    [field: SerializeField, Range(0.1f, 10)] public float MeleeCooldownTime { get; private set; }
    [field: SerializeField, Range(0.1f, 10)] public float RangeCooldownTime { get; private set; }
    [field: SerializeField, Range(0f, 500)] public float MeleeAttackForce { get; private set; }
    [field: SerializeField, Range(0f, 10)] public float Speed { get; private set; }

    private Cooldown _meleeCooldown;
    private Cooldown _rangeCooldown;

    public Cooldown MeleeCooldown => _meleeCooldown;
    public Cooldown RangeCooldown => _rangeCooldown;

    public void Init()
    {
        _meleeCooldown = new Cooldown(MeleeCooldownTime);
        _rangeCooldown = new Cooldown(RangeCooldownTime);
    }
}