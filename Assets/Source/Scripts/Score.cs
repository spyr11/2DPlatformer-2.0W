using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;
    [SerializeField] private TextMeshProUGUI _textScore;

    private void OnEnable()
    {
        _backpack.ValueChanged += AddScore;
    }

    private void OnDisable()
    {
        _backpack.ValueChanged -= AddScore;
    }

    private void AddScore(float value)
    {
        _textScore.text = value.ToString();
    }
}
