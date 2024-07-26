using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using UnityEngine;

namespace Ruinum.InstantBridge.Services
{
    public class InstantBridgeAdService : IInstantBridgeService
    {
        public void Initialize()
        {
            Bridge.advertisement.SetMinimumDelayBetweenInterstitial(60);

            Game.Context.OnGameEnded += ShowFullscreenAd;
            Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
        }

        private void ShowFullscreenAd()
        {
            Bridge.advertisement.ShowInterstitial();
        }

        private void OnInterstitialStateChanged(InterstitialState state)
        {
            Debug.Log(state);
        }

        public void Dispose()
        {
            Game.Context.OnGameEnded -= ShowFullscreenAd;
            Bridge.advertisement.interstitialStateChanged -= OnInterstitialStateChanged;
        }
    }
}