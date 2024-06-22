using System;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/PlayerData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField, Range(0, 10)] public float TopDownAttackValue { get; private set; }
    [field: SerializeField, Range(0, 10)] public float RangeAttackChargeTime { get; private set; }
    [field: SerializeField, Range(0, 50)] public float JumpForce { get; private set; }
    [field: SerializeField, Range(0, 50)] public float Speed { get; private set; }
    [field: SerializeField, Range(0, 50)] public float SpeedOnAir { get; private set; }
    [field: SerializeField, Range(0, 50)] public float vampirismValue { get; private set; }
    [field: SerializeField, Range(0, 50)] public float vampirismTime { get; private set; }

    private Cooldown _rangeCooldown;
    private Cooldown _vampirism;

    public Cooldown RangeCooldown => _rangeCooldown;
    public Cooldown Vampirism => _vampirism;

    internal void Init()
    {
        _rangeCooldown = new Cooldown(RangeAttackChargeTime);
        _vampirism = new Cooldown(vampirismTime);
    }
}
