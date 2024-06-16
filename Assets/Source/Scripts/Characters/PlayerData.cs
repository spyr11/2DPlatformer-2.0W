using System;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/PlayerData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField, Range(0, 10)] public float TopDownAttackValue { get; private set; }
    [field: SerializeField, Range(0, 10)] public float RangeAttackValue { get; private set; }
    [field: SerializeField, Range(0, 10)] public float RangeAttackChargeTime { get; private set; }
    [field: SerializeField, Range(0, 50)] public float JumpForce { get; private set; }
    [field: SerializeField, Range(0, 50)] public float Speed { get; private set; }

    private Cooldown _rangeCooldown;

    public Cooldown RangeCooldown => _rangeCooldown;

    internal void Init()
    {
        _rangeCooldown = new Cooldown(RangeAttackChargeTime);
    }
}
