using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    [SerializeField] private EnemyView _animation;
    [SerializeField] private PlayerChecker _playerChecker;
    [SerializeField] private PlayerChecker _meleeAttack;
    [SerializeField] private PlayerChecker _rangeAttack;
    [SerializeField] private Transform _patrolPoints;
    [SerializeField] private EnemyData _enemyData;

    private StateContext _stateContext;
    private Vector2[] _points;

    public override StateContext StateContext => _stateContext;

    public EnemyView Animation => _animation;
    public PlayerChecker PlayerChecker => _playerChecker;
    public PlayerChecker MeleeAttack => _meleeAttack;
    public PlayerChecker RangeAttack => _rangeAttack;
    public EnemyData Data => _enemyData;

    protected override void Init()
    {
        _enemyData.Init();

        _animation.Init();

        _points = new Vector2[_patrolPoints.childCount];

        for (int i = 0; i < _patrolPoints.childCount; i++)
        {
            _points[i] = _patrolPoints.GetChild(i).position;
        }

        _stateContext = new StateContext();
    }

    protected override void OnEnabled()
    {
        List<IState> states = new List<IState>(){
                                    new PatrolState(this,_points),
                                    new ChaseState(this),
                                    new EnemyDamagedState(this),
                                    new DieState(this,_animation),
                                    };

        if (_meleeAttack != null)
        {
            states.Add(new EnemyMeleeAttackState(this));
        }

        if (_rangeAttack != null)
        {
            states.Add(new EnemyRangeAttackState(this));
        }

        states.TrimExcess();

        _stateContext.SetStates(states);
    }

    protected override void OnDisabled() { }
}