using UnityEngine;

class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private BoxCollider2D _mapBounds;
    [SerializeField] private float _smoothTime;

    private float _minBoundX;
    private float _maxBoundX;

    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _minBoundX = _mapBounds.bounds.min.x + Camera.main.orthographicSize;
        _maxBoundX = _mapBounds.bounds.max.x - Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, _minBoundX, _maxBoundX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, 0, targetPosition.y);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }
    }
}