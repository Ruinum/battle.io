using YG;

namespace YandexGameIntegration
{
    public class YandexGMAd : IYandexGM
    {
        public void Initialize()
        {
            Game.Context.OnGameEnded += ShowFullscreenAd;
        }

        private void ShowFullscreenAd()
        {
            YandexGame.FullscreenShow();
        }

        public void Dispose()
        {
            Game.Context.OnGameEnded -= ShowFullscreenAd;
        }
    }
}