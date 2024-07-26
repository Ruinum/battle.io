using InstantGamesBridge;
using InstantGamesBridge.Modules.Game;
using UnityEngine;

namespace Ruinum.InstantBridge.Services
{
    public class InstantBridgeVisibilityService : IInstantBridgeService
    {       
        public void Initialize()
        {
            Bridge.game.visibilityStateChanged += OnGameVisibilityStateChanged;
        }

        private void OnGameVisibilityStateChanged(VisibilityState state)
        {
            switch (state)
            {
                case VisibilityState.Visible:
                    Time.timeScale = 1f;
                    AudioListener.volume = 1f;
                    break;
                case VisibilityState.Hidden:
                    Time.timeScale = 0f;
                    AudioListener.volume = 0f;
                    break;
            }
        }

        public void Dispose() { }
    }
}