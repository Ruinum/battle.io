using TMPro;
using UnityEngine;

public class SkinUnlockableUI : UnlockableUI<Skin>
{
    [SerializeField] private GameObject _select;
    [SerializeField] private GameObject _cost;
    [SerializeField] private TMP_Text _stars;

    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = Game.Context.Stats;

        OnPointerClickAction += OnPointerClick; 
        OnShowAction += Show;
    }

    private void Show()
    {
        _select.SetActive(false);
        
        if (Game.Context.PlayerSkin == _unlockable) _select.SetActive(true);
    }

    private void OnPointerClick()
    {
        if (_unlockable == null) return;
        if (_unlockable.Unlocked)
        {
            Game.Context.PlayerSkin = _unlockable;
            _select.SetActive(true);
        }

        if (!_unlockable.Unlocked)
        {
            if (_playerStats.Stars < _unlockable.Cost) return;
            
            StatsSystem.Singleton.OnStarPickedEvent(-_unlockable.Cost);
            
            _stars.text = _playerStats.Stars.ToString();
            _unlockable.Unlocked = true;
            _cost.SetActive(false);
            _blockImage.SetActive(false);

            SaveManager.Singleton.Save();
        }
    }
}