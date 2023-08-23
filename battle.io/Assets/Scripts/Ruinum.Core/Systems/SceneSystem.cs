using Ruinum.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Ruinum.Core.Systems
{
    public class SceneSystem : System<SceneSystem>
    {
        private Canvas _loadCanvas;
        private Image _blackImage;

        private Action _onSceneLoaded;
        private AsyncOperation _loadingScene;
        private AsyncOperation _unloadScene;

        private string _sceneName;
        private string _pastScene;

        private bool _isLoadScene;

        public override void Init() 
        {
            _loadCanvas = ObjectUtils.CreateGameObject<Canvas>(Game.Context.AssetsContext.GetObjectOfType(typeof(GameObject), "LoadCanvas") as GameObject);
            _blackImage = _loadCanvas.gameObject.GetComponentInChildren<Image>();

            _loadCanvas.enabled = false;
            UnityEngine.Object.DontDestroyOnLoad(_loadCanvas);
        }

        public override void Execute()
        {
            if (!_isLoadScene) return;
            if (_loadingScene.progress != 1) return;
            if (_unloadScene.progress != 1) return;

            OnSceneComplete();
        }

        public void LoadScene(string name, Action onSceneLoaded = null)
        {
            _sceneName = name;
            _onSceneLoaded = onSceneLoaded;
            _pastScene = SceneManager.GetActiveScene().name;

            Debug.Log($"Loading {_sceneName}, Past {_pastScene}");

            SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive).completed += LoadingScene;
        }

        private void LoadingScene(AsyncOperation asyncOperation)
        {
            _loadCanvas.enabled = true;
           
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading"));

            _blackImage.DOFillAmount(1, 1).OnComplete(() =>
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

            _blackImage.DOFillAmount(0, 1).OnComplete(() => 
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));
                _loadCanvas.enabled = false;
            });
        }
    }
}