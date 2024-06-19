using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Picker))]
public class Player : BaseCharacter, IHealable
{
    [field: SerializeField] public PlayerData Data { get; private set; }
    [field: SerializeField] public PlayerView Animation { get; private set; }
    [field: SerializeField] public GroundDetector GroundChecker { get; private set; }
    [field: SerializeField] public EnemyFinder EnemyFinder { get; private set; }

    private StateContext _stateContext;
    private PlayerInput _playerInput;
    private Picker _picker;

    public event Action<float> Healed;

    public override StateContext StateContext => _stateContext;

    public bool AttackPressed => _playerInput.Action.Attack.IsPressed();
    public bool JumpPressed => _playerInput.Action.Jump.IsPressed();

    public float ReadHorizontal() => _playerInput.Action.Move.ReadValue<float>();

    protected override void Init()
    {
        Data.Init();

        Animation.Init();

        _playerInput = new PlayerInput();

        _stateContext = new StateContext();

        _picker = GetComponent<Picker>();
    }

    protected override void OnEnabled()
    {
        _playerInput.Enable();

        List<IState> states = new List<IState>(){
                                    new IdleState(this),
                                    new WalkState(this),
                                    new JumpState(this),
                                    new FallState(this),
                                    new TopDownAttackState(this),
                                    new RangeAttackState(this),
                                    new DamagedState(this),
                                    new DieState(this),
                                    };

        states.TrimExcess();

        _stateContext.SetStates(states);

        _picker.HealPicked += Heal;
    }

    protected override void OnDisabled()
    {
        _playerInput.Disable();

        _picker.HealPicked -= Heal;
    }

    private void Heal(float value)
    {
        Healed?.Invoke(value);
    }
}
