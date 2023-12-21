using System;
using System.Collections.Generic;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [SerializeField] private LocalizationConfig _config;

    private Dictionary<string, TextData[]> _localizationData = new Dictionary<string, TextData[]>();

    private LanguageEnum _currentLanguage = LanguageEnum.RU;
    public Action OnLanguageChanged;
    public static Localization Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        for (int i = 0; i < _config.LocalizationDatas.Length; i++)
        {
            var localizationData = _config.LocalizationDatas[i];
            _localizationData.Add(localizationData.Key, localizationData.Values);
        }
    }

    public bool GetText(string key, out string text)
    {
        text = null;

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

        return true;
    }

    public bool GetTextData(string key, out TextData[] textDatas)
    {
        return _localizationData.TryGetValue(key, out textDatas);
    }

    public void ChangeLanguage(LanguageEnum language)
    {
        _currentLanguage = language;
        OnLanguageChanged?.Invoke();
    }
}