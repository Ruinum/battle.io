using InstantGamesBridge;
using UnityEngine;

namespace Ruinum.InstantBridge.Services
{
    public class InstantBridgeSavingService : IInstantBridgeService, ISave
    {
        private string _loadedData;
        public void Initialize() 
        {
            SaveManager.Singleton.SetSaveImplementation(this);
            SaveManager.Singleton.Load();
        }

        public void Save(string text)
        {
            Bridge.storage.Set("Data", text, OnStorageSetCompleted);
        }

        private void OnStorageSetCompleted(bool success)
        {
            Debug.Log($"OnStorageSetCompleted, success: {success}");
        }

        public bool Load(out string savedData)
        {
            savedData = "";

            Bridge.storage.Get("Data", OnStorageGetCompleted);

            savedData = _loadedData;
            if(string.IsNullOrEmpty(savedData)) return false;
            return true;
        }

        private void OnStorageGetCompleted(bool success, string data)
        {
            Debug.Log($"Loading storage.");

            if(!success) { Debug.Log($"Loading of storage eneded with exception."); return; }
            if (string.IsNullOrEmpty(data)) { Debug.Log($"Data in storage null or empty."); return; }
            
            _loadedData = data;
        }

        public void Dispose() { }
    }
}