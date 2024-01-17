using YG;

namespace YandexGameIntegration
{
    public class YandexLocalization : IYandexGM
    {
        public void Initialize()
        {
            ChangeLocalization();
        }

        private void ChangeLocalization()
        {
            var language = YandexGame.EnvironmentData.language;
            switch (language)
            {
                case "en": Localization.Singleton.ChangeLanguage(LanguageEnum.EN); break;
                case "ru": Localization.Singleton.ChangeLanguage(LanguageEnum.RU); break;
                case "tr": Localization.Singleton.ChangeLanguage(LanguageEnum.TR); break;
                default: Localization.Singleton.ChangeLanguage(LanguageEnum.EN); break;
            }
        }

        public void Dispose() { }
    }
}