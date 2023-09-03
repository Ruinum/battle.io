using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private bool _initialize = true;

    private void Start()
    {
        if (_initialize) SceneManager.LoadScene(_sceneName);
    }

    public void LoadScene() => SceneManager.LoadScene(_sceneName);
}