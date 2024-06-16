using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    private bool _isGrounded;

    public BoxCollider2D Box => _boxCollider2D;
    public bool IsGrounded => _isGrounded;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Ground>(out _))
        {
            _isGrounded = true;
        }

        _boxCollider2D = null;

        if (other.TryGetComponent(out Platform platform) && platform.TryGetComponent(out _boxCollider2D)) { }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isGrounded = false;
    }
}