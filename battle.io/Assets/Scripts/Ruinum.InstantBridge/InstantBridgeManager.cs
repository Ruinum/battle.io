using System.Collections.Generic;
using Ruinum.InstantBridge.Services;
using UnityEngine;

namespace Ruinum.InstantBridge
{
    public class InstantBridgeManager : MonoBehaviour
    {
        private List<IInstantBridgeService> _services;

        private void Awake()
        {
            _services = new List<IInstantBridgeService>();

            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _services.Add(new InstantBridgeSavingService());
            _services.Add(new InstantBridgeMessageService());
            _services.Add(new InstantBridgeRewardService());
            _services.Add(new InstantBridgeAdService());
            _services.Add(new InstantBridgeLocalizationService());
            _services.Add(new InstantBridgeVisibilityService());

            for (int i = 0; i < _services.Count; i++)
            {
                _services[i].Initialize();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _services.Count; i++)
            {
                _services[i].Dispose();
            }
        }        
    }
}