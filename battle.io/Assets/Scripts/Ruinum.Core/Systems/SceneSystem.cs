using Ruinum.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace Ruinum.Core.Systems
{
    public class SceneSystem : System<SceneSystem>
    {
        private LoadScreen _loadScreen;

        private Action _onSceneLoaded;
        private AsyncOperation _loadingScene;
        private AsyncOperation _unloadScene;

        private string _sceneName;
        private string _pastScene;

        private bool _isLoadScene;

        public override void Initialize() 
        {
            _loadScreen = ObjectUtils.CreateGameObject<LoadScreen>(Game.Context.AssetsContext.GetObjectOfType(typeof(GameObject), "LoadCanvas") as GameObject);
        }

        public override void Execute()
        {
            if (!_isLoadScene) return;
            if (!_loadingScene.isDone || !_unloadScene.isDone) return;

            OnSceneComplete();
        }

        public void LoadScene(string name, Action onSceneLoaded = null)
        {
            _sceneName = name;
            _onSceneLoaded = onSceneLoaded;
            _pastScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive).completed += LoadingScene;
            ExecuteSystem.Singleton.ClearAllExecuteObjects();
        }

        private void LoadingScene(AsyncOperation asyncOperation)
        {        
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading"));
            
            _loadScreen.Show(() =>
            {
                _loadingScene = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
                _unloadScene = SceneManager.UnloadSceneAsync(_pastScene);

                _isLoadScene = true;
            });
        }

        private void OnSceneComplete()
        {
            _isLoadScene = false;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));

            _onSceneLoaded?.Invoke();
            _loadScreen.Hide(() =>
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));
            });           
        }
    }
}