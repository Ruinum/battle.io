using Ruinum.Core.Systems;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    private bool _isShow;

    public void Show()
    {
        _window.SetActive(true);
        _isShow = true;
    }

    public void Hide()
    {
        _window.SetActive(false);
        _isShow = false;
    }

    public void LoadMenu()
    {
        SceneSystem.Singleton.LoadScene("Menu", () => Game.Context.EndGame());    
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isShow) Show();
    }
}
