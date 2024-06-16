using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _map;
    [SerializeField] private Transform _coin;
    [SerializeField, Range(0, 20)] private int _count;
    [SerializeField] private float _maxPositionY = 10f;

    private void Start()
    {
        _map = GetComponent<BoxCollider2D>();

        float mapSizeX = _map.bounds.max.x - _map.bounds.min.x;
        float offset = mapSizeX / _count;

        float positionX = _map.bounds.min.x;
        float positionY = _map.bounds.max.y + _maxPositionY;

        for (uint i = 0; i < _count; i++)
        {
            Vector3 position = new Vector3(positionX, positionY, 0);

            Instantiate(_coin, position, Quaternion.identity);

            positionX += offset;
        }
    }
}
