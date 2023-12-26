using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [SerializeField] private LocalizationConfig _localization;
    [SerializeField] private SkinsConfig _skins;
    [SerializeField] private AchievementsConfig _achievements;
    [SerializeField] private LanguageEnum _currentLanguage = LanguageEnum.RU;

    [SerializeField] private Sprite _russianLanguageIcon;
    [SerializeField] private Sprite _englishLanguageIcon;
    [SerializeField] private Sprite _turkishLanguageIcon;

    private Dictionary<string, TextData[]> _localizationData = new Dictionary<string, TextData[]>();
    
    public Action OnLanguageChanged;
    public static Localization Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        for (int i = 0; i < _localization.LocalizationDatas.Length; i++)
        {
            var localizationData = _localization.LocalizationDatas[i];
            if (_localizationData.ContainsKey(localizationData.Key)) continue;
            _localizationData.Add(localizationData.Key, localizationData.Values);
        }
    
        for (int i = 0; i < _skins.Skins.Length; i++)
        {
            var skin = _skins.Skins[i];
            if (!_localizationData.ContainsKey(skin.NameLocalization.Key))
            {
                _localizationData.Add(skin.NameLocalization.Key, skin.NameLocalization.Values);
            }

            if (!_localizationData.ContainsKey(skin.DescriptionLocalization.Key))
            {
                _localizationData.Add(skin.DescriptionLocalization.Key, skin.DescriptionLocalization.Values);
            }
        }

        for (int i = 0; i < _achievements.Achievements.Length; i++)
        {
            var achievement = _achievements.Achievements[i];
            if (!_localizationData.ContainsKey(achievement.NameLocalization.Key))
            {
                _localizationData.Add(achievement.NameLocalization.Key, achievement.NameLocalization.Values);
            }

            if (!_localizationData.ContainsKey(achievement.DescriptionLocalization.Key))
            {
                _localizationData.Add(achievement.DescriptionLocalization.Key, achievement.DescriptionLocalization.Values);
            }
        }
    }

    public void ChangeLanguage(LanguageEnum language)
    {
        _currentLanguage = language;
        OnLanguageChanged?.Invoke();
    }

    public bool GetText(string key, out string text, out TMP_FontAsset font)
    {
        text = null;
        font = null;

        if (!GetTextData(key, out var data))
        {
            if (EditorConstants.Logging) Debug.LogWarning($"There is no localization for {key} in {_currentLanguage} language");
            return false;
        }

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].Language != _currentLanguage) continue;
            text = data[i].Text;

            break;
        }

        switch (_currentLanguage)
        {
            case LanguageEnum.RU: font = _localization.RussianFont; break;
            case LanguageEnum.EN: font = _localization.EnglishFont; break;
            case LanguageEnum.TR: font = _localization.TurkishFont; break;
        }

        return true;
    }

    public bool GetTextData(string key, out TextData[] textDatas)
    {
        return _localizationData.TryGetValue(key, out textDatas);
    }

    public Sprite GetLanguageIcon()
    {
        switch (_currentLanguage)
        {
            case LanguageEnum.RU: return _russianLanguageIcon; break;
            case LanguageEnum.EN: return _englishLanguageIcon; break;
            case LanguageEnum.TR: return _turkishLanguageIcon; break;
        }

        return _englishLanguageIcon;
    }
}