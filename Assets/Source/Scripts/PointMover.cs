using UnityEngine;

public class PointMover : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _maxSpeed;

    private Transform[] _points;
    private int _currentIndex;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _path.GetChild(i);
        }

        _currentIndex = Random.Range(0, _points.Length);
    }

    private void Update()
    {
        MoveToPosition();
    }

    private void MoveToPosition()
    {
        Vector3 targetPosition = _points[_currentIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _maxSpeed * Time.deltaTime);

        float minDistance = 0.1f;

        if (Vector3.SqrMagnitude(transform.position - targetPosition) < minDistance)
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
