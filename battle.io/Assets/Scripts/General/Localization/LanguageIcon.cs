using UnityEngine;
using UnityEngine.UI;

public class LanguageIcon : MonoBehaviour
{
    [SerializeField] private Image _languageIcon;
    
    private void Start()
    {
        OnLanguageChange();
        Localization.Singleton.OnLanguageChanged += OnLanguageChange;
        _languageIcon = GetComponent<Image>();
    }

    private void OnLanguageChange()
    {
        _languageIcon.sprite = Localization.Singleton.GetLanguageIcon();
    }

    private void OnDestroy()
    {
        Localization.Singleton.OnLanguageChanged -= OnLanguageChange;
    }
}