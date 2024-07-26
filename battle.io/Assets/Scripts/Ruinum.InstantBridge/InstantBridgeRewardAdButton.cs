using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using UnityEngine;
using UnityEngine.UI;

namespace Ruinum.InstantBridge
{
    [RequireComponent(typeof(Button))]
    public class InstantBridgeRewardAdButton : RewardAdButton
    {
        [SerializeField] private SkinWindow _skinWindow;
        [SerializeField] private GameObject _blockUi;

        private bool _subscribed = false;

        public void OpenRewardAd()
        {
            if (_currentSkin == null) { Debug.LogError($"Skin isn't selected. Please check logic, {this}"); return; }

            _blockUi.SetActive(true);

            if (!_subscribed)
            {
                Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;
                _subscribed = true;
            }

            Bridge.advertisement.ShowRewarded();
        }

        private void OnRewardedStateChanged(RewardedState state)
        {
            switch (state)
            {
                case RewardedState.Opened:
                    OnAdStarted();
                    break;
                case RewardedState.Rewarded:
                    OnAdRewarded();
                    break;
                case RewardedState.Closed:
                    OnAdClosed();
                    break;
                case RewardedState.Failed:
                    OnAdError();
                    break;
                case RewardedState.Loading:
                    Time.timeScale = 0f;
                    AudioListener.pause = true;
                    break;
            }
        }
        
        private void OnAdStarted()
        {
            _blockUi.SetActive(true);
        }

        private void OnAdClosed()
        {
            _blockUi.SetActive(false);
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }

        private void OnAdRewarded()
        {
            _currentSkin.Unlocked = true;
            _skinWindow.Show();
            SaveManager.Singleton.Save();
        }

        private void OnAdError()
        {
            Debug.LogWarning($"CrazyGames reward ad not been displayed, {this}");
            
            Time.timeScale = 1f;
            AudioListener.pause = false;
            _blockUi.SetActive(false);
        }
    }
}
