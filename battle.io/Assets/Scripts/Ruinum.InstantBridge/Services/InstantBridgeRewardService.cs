using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using UnityEngine;

namespace Ruinum.InstantBridge.Services
{
    public class InstantBridgeRewardService : IInstantBridgeService
    {
        public void Initialize()
        {
            Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;
        }

        private void OnRewardedStateChanged(RewardedState state)
        {
            Debug.Log(state.ToString());
            switch (state)
            {
                case RewardedState.Opened:
                    Time.timeScale = 0f;
                    AudioListener.volume = 0f;
                    break;
                case RewardedState.Loading:
                    Time.timeScale = 0f;
                    AudioListener.volume = 0f;
                    break;
                case RewardedState.Closed:
                    Time.timeScale = 1f;
                    AudioListener.volume = 1f;
                    break;
                case RewardedState.Failed:
                    Time.timeScale = 1f;
                    AudioListener.volume = 1f;
                    break;
                case RewardedState.Rewarded:
                    Time.timeScale = 1f;
                    AudioListener.volume = 1f;
                    break;
            }
        }

        public void Dispose()
        {
            Bridge.advertisement.rewardedStateChanged -= OnRewardedStateChanged;
        }
    }
}