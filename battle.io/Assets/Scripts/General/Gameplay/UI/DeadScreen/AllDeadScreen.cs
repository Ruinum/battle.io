using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Ruinum.Core.Systems;

public class AllDeadScreen : MonoBehaviour
{    
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private float _timeFade;

    private Image[] _images;
    private TMP_Text[] _texts;

    private IEnumerator Start()
    {
        _images = GetComponentsInChildren<Image>();
        _texts = GetComponentsInChildren<TMP_Text>();
        _deathScreen.SetActive(false);

        yield return new WaitForEndOfFrame();

        Game.Context.Player.Level.OnDead += PlayDeadScreen;
    }

    public void ToMenu()
    {
        SceneSystem.Singleton.LoadScene("Menu", Game.Context.EndGame);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayDeadScreen(Level level)
    {
        _deathScreen.SetActive(true);
        
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
