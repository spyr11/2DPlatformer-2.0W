using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public IDamagable Player => _player;
    public bool IsDetected => _player != null;

    private IDamagable _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable player) && player is Player)
        {
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable player) && player is Player)
        {
            _player = null;
        }
    }
}