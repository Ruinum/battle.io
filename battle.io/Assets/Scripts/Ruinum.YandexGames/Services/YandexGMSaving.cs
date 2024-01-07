using UnityEngine;
using YG;

namespace YandexGameIntegration
{
    public class YandexGMSaving : IYandexGM, ISave
    {
        public void Initialize() 
        {
            SaveManager.Singleton.SetSaveImplementation(this);
            SaveManager.Singleton.Load();
        }

        public void Save(string text)
        {
            YandexGame.savesData.SaveData = text;
            YandexGame.SaveProgress();
        }

        public bool Load(out string savedData)
        {
            savedData = "";

            YandexGame.LoadProgress();
            var yandexSavedData = YandexGame.savesData.SaveData;
            if (string.IsNullOrEmpty(yandexSavedData) || yandexSavedData == default) 
            {
                if (EditorConstants.Logging) Debug.LogWarning($"There is no saved data in Yandex cloud, {typeof(YandexGMSaving)}");
                return false;
            }

            savedData = YandexGame.savesData.SaveData;
            return true;
        }

        public void Dispose() { }
    }
}