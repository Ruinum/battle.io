using UnityEngine;
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
            StatsSystem.Singleton.OnStarPickedEvent(5);
            
            SaveManager.Singleton.Save();
        }

        public void Dispose()
        {
            YandexGame.RewardVideoEvent -= Reward;
        }
    }
}