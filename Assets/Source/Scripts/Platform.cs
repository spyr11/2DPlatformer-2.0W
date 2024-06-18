using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Transform _path;

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

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
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
