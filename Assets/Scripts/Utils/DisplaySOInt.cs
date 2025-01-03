using UnityEngine;
using TMPro;

public class DisplaySOInt : MonoBehaviour
{
    [SerializeField] private SOInt _soInt;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _soInt.OnValueChanged += OnValueChanged;
        OnValueChanged(_soInt.Value);
    }

    private void OnDestroy()
    {
        _soInt.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _text.text = value.ToString();
    }
}