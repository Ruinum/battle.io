using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LocalizationView : MonoBehaviour
{
    [SerializeField] private string _key;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        LanguageChanged();
        Localization.Singleton.OnLanguageChanged += LanguageChanged;
    }

    private void LanguageChanged()
    {
        if (!Localization.Singleton.GetText(_key, out string text, out TMP_FontAsset font)) return;
        _text.text = text;
        _text.font = font;
    }

    private void OnDestroy()
    {
        Localization.Singleton.OnLanguageChanged -= LanguageChanged;
    }
}