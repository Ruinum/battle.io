using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InstantGamesBridge
{
    public class InstantBridgeManager : MonoBehaviour
    {
        private List<IInstantBridgeGM> _services;

        private void Awake()
        {
            _services = new List<IInstantBridgeGM>();

            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _services.Add(new InstantBridgeGMSaving());
            _services.Add(new InstantBridgeGMReward());
            _services.Add(new InstantBridgeGMAd());
            _services.Add(new InstantBridgeLocalization());
            _services.Add(new InstantBridgeVisibility());

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