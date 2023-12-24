using UnityEngine;
using UnityEngine.UI;

public class LanguageIcon : MonoBehaviour
{
    [SerializeField] private Image _languageIcon;
    
    private void Start()
    {
        OnLanguageChange();
        Localization.Singleton.OnLanguageChanged += OnLanguageChange;   
    }

    private void OnLanguageChange()
    {
        Debug.Log(Localization.Singleton.GetLanguageIcon());
        _languageIcon.sprite = Localization.Singleton.GetLanguageIcon();
    }
}