using UnityEngine;

public class TreeItemView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private GameObject _blockImage;
    [SerializeField] private Unlockable _unlockable;

    public void Initialize()
    {
        Hide();

        if (_unlockable == null || _unlockable == default) return;
        if (_unlockable.Icon != null) _icon.sprite = _unlockable.Icon;
        if (_unlockable.Unlocked) Show();
    }

    public void Show() { _blockImage.SetActive(false); }
    public void Hide() { _blockImage.SetActive(true); }
}
