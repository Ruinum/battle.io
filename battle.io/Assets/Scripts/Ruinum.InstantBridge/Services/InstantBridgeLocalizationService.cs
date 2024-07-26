using InstantGamesBridge;

namespace Ruinum.InstantBridge.Services
{
    public class InstantBridgeLocalizationService : IInstantBridgeService
    {
        public void Initialize()
        {
            ChangeLocalization();
        }

        private void ChangeLocalization()
        {
            var language = Bridge.platform.language;
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