using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace YandexGameIntegration
{
    public class YandexGM : MonoBehaviour
    {
        private List<IYandexGM> _yandexServices;

        private void Awake()
        {
            _yandexServices = new List<IYandexGM>();

            DontDestroyOnLoad(this);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.05f);

            if (YandexGame.SDKEnabled == true)
            {              
                InitializeYandexSystems();
                YandexGame.GameReadyAPI();
            }
        }

        public void InitializeYandexSystems()
        {
            _yandexServices.Add(new YandexGMSaving());
            _yandexServices.Add(new YandexGMReward());
            _yandexServices.Add(new YandexGMAd());
            _yandexServices.Add(new YandexLocalization());

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