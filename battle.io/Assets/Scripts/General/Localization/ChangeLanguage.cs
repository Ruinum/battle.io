using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public void ChangeToRu()
    {
        Change(LanguageEnum.RU);
    }

    public void ChangeToEn()
    {
        Change(LanguageEnum.EN);
    }

    public void ChangeToTr()
    {
        Change(LanguageEnum.TR);
    }

    private void Change(LanguageEnum language)
    {
        switch (language)
        {
            case LanguageEnum.RU: Localization.Singleton.ChangeLanguage(LanguageEnum.RU); break;
            case LanguageEnum.EN: Localization.Singleton.ChangeLanguage(LanguageEnum.EN); break;
            case LanguageEnum.TR: Localization.Singleton.ChangeLanguage(LanguageEnum.TR); break;
        }
    }
}