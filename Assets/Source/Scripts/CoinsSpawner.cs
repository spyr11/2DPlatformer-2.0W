using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _map;
    [SerializeField] private Coin _coin;
    [SerializeField] private uint _count;

    private void Start()
    {
        float mapSizeX = _map.bounds.max.x - _map.bounds.min.x;
        float offset = mapSizeX / _count;

        float positionX = _map.bounds.min.x;
        float positionY = _map.bounds.max.y;

        for (uint i = 0; i < _count; i++)
        {
            var position = new Vector3(positionX, positionY, 0);

            Instantiate(_coin, position, Quaternion.identity);

            positionX += offset;
        }
    }
}
