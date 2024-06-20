using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public Platform Platform { get; private set; }
    public bool IsGrounded { get; private set; }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Ground>(out _))
        {
            IsGrounded = true;
        }

        Platform = null;

        if (other.TryGetComponent(out Platform platform))
        {
            Platform = platform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Ground>(out _))
        {
            IsGrounded = false;
        }
    }
}