using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    [SerializeField] private Transform _patrolPoints;
    [field: SerializeField] public EnemyView Animation { get; private set; }
    [field: SerializeField] public PlayerFinder PlayerFinder { get; private set; }
    [field: SerializeField] public PlayerFinder MeleeAttack { get; private set; }
    [field: SerializeField] public PlayerFinder RangeAttack { get; private set; }
    [field: SerializeField] public EnemyData Data { get; private set; }

    private StateContext _stateContext;
    private Vector2[] _points;

    public override StateContext StateContext => _stateContext;

    protected override void Init()
    {
        Data.Init();

        Animation.Init();

        _points = GetPoints(_patrolPoints);

        _stateContext = new StateContext();
    }

    protected override void OnEnabled()
    {
        List<IState> states = new List<IState>(){
                                    new PatrolState(this,_points),
                                    new ChaseState(this),
                                    new EnemyDamagedState(this),
                                    new DieState(this),
                                    };

        if (MeleeAttack != null)
        {
            states.Add(new EnemyMeleeAttackState(this));
        }

        if (RangeAttack != null)
        {
            states.Add(new EnemyRangeAttackState(this));
        }

        states.TrimExcess();

        _stateContext.SetStates(states);
    }

    protected override void OnDisabled() { }

    private Vector2[] GetPoints(Transform patrolPoints)
    {
        Vector2[] points = new Vector2[patrolPoints.childCount];

        for (int i = 0; i < patrolPoints.childCount; i++)
        {
            points[i] = patrolPoints.GetChild(i).position;
        }

        return points;
    }
}