using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private Image _blackScreen;
    [SerializeField] private TMP_Text _text;

    private Color _showColor = new Color(255, 255, 255, 255);
    private Color _hideColor = new Color(255, 255, 255, 0);

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(Action onEnd)
    {
        _blackScreen.DOFillAmount(1, 1).OnComplete(onEnd.Invoke);
        _text.DOColor(_showColor, 0.75f);

        gameObject.SetActive(true);
    }

    public void Hide(Action onEnd)
    {
        _blackScreen.DOFillAmount(0, 1).OnComplete(() => { onEnd?.Invoke(); gameObject.SetActive(false); });
        _text.DOColor(_hideColor, 0.1f);
    }
}
