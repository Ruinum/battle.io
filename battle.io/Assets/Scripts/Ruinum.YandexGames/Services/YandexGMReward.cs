using YG;

namespace YandexGameIntegration
{
    public class YandexGMReward : IYandexGM
    {
        public void Initialize()
        {
            YandexGame.RewardVideoEvent += Reward;
        }

        private void Reward(int id)
        {
            //Logic for adding stars
            StatsSystem.Singleton.OnStarPickedEvent(5);
        }

        public void Dispose()
        {
            YandexGame.RewardVideoEvent -= Reward;
        }
    }

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