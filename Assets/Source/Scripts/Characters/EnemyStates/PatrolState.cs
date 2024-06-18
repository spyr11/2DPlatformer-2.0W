using UnityEngine;

public class PatrolState : EnemyMovementState
{
    private readonly Vector2[] _points;
    private readonly int _startPointIndex;

    private int _currentIndex;

    public PatrolState(Enemy enemy, Vector2[] points) : base(enemy)
    {
        _points = points;
        _startPointIndex = Random.Range(0, points.Length);
    }

    public override void Enter()
    {
        base.Enter();

        _currentIndex = _startPointIndex;

        Enemy.Animation.StartPatrolling();
    }

    public override void Exit()
    {
        base.Exit();

        Enemy.Animation.StopPatrolling();
    }

    public override void Update()
    {
        base.Update();

        if (Enemy.PlayerChecker.IsDetected)
        {
            StateSwitcher.Switch<ChaseState>();
        }

        SetNewPosition();
    }

    private void SetNewPosition()
    {
        Vector2 targetPosition = _points[_currentIndex];
        Vector2 direction = (targetPosition - Enemy.Rigidbody2D.position);

        SetDirectionAndSpeed(direction, Data.Speed, Data.Speed);

        if (Mathf.Abs(targetPosition.x - Enemy.Rigidbody2D.position.x) < 0.1f)
        {
            RiseIndex();
        }
    }

    private void RiseIndex()
    {
        _currentIndex++;

        if (_currentIndex >= _points.Length)
        {
            _currentIndex = 0;
        }
    }
}


