using TMPro;
using UnityEngine;

public class TextBar : Bar
{
    [SerializeField] private string _description = "Text bar:";

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public override void ChangeValue()
    {
        _text.text = $"{_description} {Health.Current} / {Health.Max}";
    }
}
