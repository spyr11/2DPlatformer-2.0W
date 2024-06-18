using UnityEngine;

public class PlayerChecker : MonoBehaviour
{

    public IDamagable Player => _player;
    public bool IsDetected => _player != null;

    private IDamagable _player;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable player) && player is Player)
        {
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable player) && player is Player)
        {
            _player = null;
        }
    }
}