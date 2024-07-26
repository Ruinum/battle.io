using InstantGamesBridge;
using InstantGamesBridge.Modules.Platform;

namespace Ruinum.InstantBridge.Services
{
    public class InstantBridgeMessageService : IInstantBridgeService
    {
        public void Initialize()
        {
            Game.Context.OnGameStarted += OnGameStarted;
            Game.Context.OnGameEnded += OnGameEnded;
        }

        private void OnGameStarted()
        {
            Bridge.platform.SendMessage(PlatformMessage.GameplayStarted);
        }
        
        private void OnGameEnded()
        {
            Bridge.platform.SendMessage(PlatformMessage.GameplayStopped);
        }
        
        public void Dispose()
        {
            Game.Context.OnGameStarted -= OnGameStarted;
            Game.Context.OnGameEnded -= OnGameEnded;
        }
    }
}