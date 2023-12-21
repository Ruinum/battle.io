using System.Collections.Generic;
using UnityEngine;
using YG;

namespace YandexGameIntegration
{
    //Adapter of YandexGame
    public class YandexGM : MonoBehaviour
    {
        private List<IYandexGM> _yandexServices;

        private void Awake()
        {
            _yandexServices = new List<IYandexGM>();

            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            YandexGame.GameReadyAPI();

            _yandexServices.Add(new YandexGMSaving());
            _yandexServices.Add(new YandexGMReward());
            _yandexServices.Add(new YandexGMAd());

            for (int i = 0; i < _yandexServices.Count; i++)
            {
                _yandexServices[i].Initialize();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _yandexServices.Count; i++)
            {
                _yandexServices[i].Dispose();
            }
        }
    }
}