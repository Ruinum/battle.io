using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Ruinum.Utils;
using System;

public class UnlockableUI<T> : MonoBehaviour, IPointerClickHandler where T : Unlockable
{
    [SerializeField] protected Image _image;
    [SerializeField] protected GameObject _blockImage;
    [SerializeField] protected AudioConfig _audioConfig;
    [SerializeField] protected TMP_Text _name;
    [SerializeField] protected TMP_Text _description;

    protected T _unlockable;
    private Vector3 _punchScale = new Vector3(0.15f, 0.15f, 0.15f);

    protected Action OnPointerClickAction;
    protected Action OnHideAction;
    protected Action OnShowAction;

    public void Show(T unlockable)
    {
        Hide();
        _image.sprite = unlockable.Icon;
        _unlockable = unlockable;

        if (unlockable.Unlocked) _blockImage.SetActive(false);

        OnShowAction?.Invoke();
    }

    public void Hide()
    {
        _blockImage.SetActive(true);
        OnHideAction?.Invoke();
    }

    public void ChangeText()
    {
        _name.text = _unlockable.Name;
        _description.text = _unlockable.Description;

        if (!_unlockable.Unlocked) { _name.text = "???"; _description.text = "???"; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOPunchScale(_punchScale, 0.25f).OnComplete(() => transform.DOScale(new Vector3(1, 1, 1), 0.15f));
        AudioUtils.PlayAudio(_audioConfig, transform.position);

        ChangeText();

        OnPointerClickAction?.Invoke();
    }
}