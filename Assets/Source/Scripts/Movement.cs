using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _maxSpeed;

    public event UnityAction<float> OnSpeedChanged;
    public event UnityAction<bool> OnJumpingChanged;

    private float _speed;
    private float _jumpForce;
    private bool _isJumping;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _jumpForce = 600f; ;
        _speed = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false)
        {
            _isJumping = true;
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);

            OnJumpingChanged?.Invoke(_isJumping);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            _speed = -_maxSpeed;
            OnSpeedChanged?.Invoke(_speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _speed = _maxSpeed;
            OnSpeedChanged?.Invoke(_speed);

        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _speed = 0f;
            OnSpeedChanged?.Invoke(_speed);
        }

        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isJumping = false;
        OnJumpingChanged?.Invoke(_isJumping);
    }

    private void Move()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + _speed, transform.position.y), Time.deltaTime);
    }
}