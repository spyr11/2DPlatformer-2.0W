using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private CharacterView _animation;
    [SerializeField] private EnemyChecker _enemyChecker;
    [SerializeField] private GroundChecker _groundChecker;

    private StateContext _stateContext;
    private NewControls _playerInput;

    public bool AttackPressed => _playerInput.Action.Attack.IsPressed();
    public bool JumpPressed => _playerInput.Action.Jump.IsPressed();

    public PlayerData Data => _playerData;
    public CharacterView Animation => _animation;
    public EnemyChecker EnemyChecker => _enemyChecker;
    public GroundChecker GroundChecker => _groundChecker;

    public override StateContext StateContext => _stateContext;

    protected override void Init()
    {
        _playerData.Init();

        _animation.Init();

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
                                    new DieState(this,_animation),
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
