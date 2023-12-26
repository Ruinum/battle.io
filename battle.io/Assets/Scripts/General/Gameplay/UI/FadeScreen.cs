using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Ruinum.Core.Systems;

public class FadeScreen : MonoBehaviour
{    
    [SerializeField] private GameObject _fadeScreen;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private float _timeFade;

    private Image[] _images;
    private TMP_Text[] _texts;

    private IEnumerator Start()
    {
        _images = GetComponentsInChildren<Image>();
        _texts = GetComponentsInChildren<TMP_Text>();
        _fadeScreen.SetActive(false);

        yield return new WaitForEndOfFrame();

        Game.Context.Player.Level.OnDead += OnDied;
        Game.Context.OnFinalStageEnded += OnGameWin;
    }

    public void ToMenu()
    {
        SceneSystem.Singleton.LoadScene("Menu", Game.Context.EndGame);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnDied(Level level)
    {
        if (!Localization.Singleton.GetText("PLAYER_DIED_MESSAGE", out string text, out var font)) _message.text = "You Died!";
        
        _message.text = text;
        _message.font = font;

        FadeIn();
    }

    private void OnGameWin()
    {
        if (!Localization.Singleton.GetText("PLAYER_WIN_MESSAGE", out string text, out var font)) _message.text = "You Win!";

        _message.text = text;
        _message.font = font;

        FadeIn();
    }

    private void FadeIn()
    {
        if (_fadeScreen == null) return;
        _fadeScreen.SetActive(true);

        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].DOFade(1, _timeFade);
        }

        for (int i = 0; i < _texts.Length; i++)
        {
            _texts[i].DOFade(1, _timeFade);
        }
    }
}
