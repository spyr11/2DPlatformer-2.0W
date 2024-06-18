using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [field: SerializeField] public PlayerData Data { get; private set; }
    [field: SerializeField] public PlayerView Animation { get; private set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }
    [field: SerializeField] public EnemyChecker EnemyChecker { get; private set; }

    private StateContext _stateContext;
    private NewControls _playerInput;

    public override StateContext StateContext => _stateContext;

    public bool AttackPressed => _playerInput.Action.Attack.IsPressed();
    public bool JumpPressed => _playerInput.Action.Jump.IsPressed();

    protected override void Init()
    {
        Data.Init();

        Animation.Init();

        _playerInput = new NewControls();

        _stateContext = new StateContext();
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
    }

    protected override void OnDisabled()
    {
        _playerInput.Disable();
    }

    public float ReadHorizontal() => _playerInput.Action.Move.ReadValue<float>();
}
