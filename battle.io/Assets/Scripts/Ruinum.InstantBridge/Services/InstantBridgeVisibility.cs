using InstantGamesBridge.Modules.Game;
using UnityEngine;

namespace InstantGamesBridge
{
    public class InstantBridgeVisibility : IInstantBridgeGM
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