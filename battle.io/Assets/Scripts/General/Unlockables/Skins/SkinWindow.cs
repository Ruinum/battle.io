using DG.Tweening;
using System.Linq;
using Ruinum.InstantBridge;
using TMPro;
using UnityEngine;

public class SkinWindow : MonoBehaviour
{
    [SerializeField] private GameObject _cost;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _starsText;
    [SerializeField] private SkinUnlockableUI _skinView;
    [SerializeField] private SkinsConfig _skinsConfig;
    [SerializeField] private GameObject _rewardAdButtonObject;
    [SerializeField] protected RewardAdButton _rewardAdButton;

    private Skin[] _skins;

    private Vector3 _punchScale = new Vector3(0.15f, 0.15f, 0.15f);
    private int _count = 0;

    private void Awake()
    {
        _skins = _skinsConfig.Skins.ToArray();
    }

    public void Show()
    {
        var stats = Game.Context.Stats;

        _costText.text = _skins[_count].Cost.ToString();
        _starsText.text = stats.Stars.ToString();

        _cost.SetActive(true);

        if (_skins[_count].Unlocked) _cost.SetActive(false);
        _skinView.transform.DOPunchScale(_punchScale, 0.25f).OnComplete(() => _skinView.transform.DOScale(new Vector3(1, 1, 1), 0.15f));

        if (_skins[_count].Unlocked)
        {
            _rewardAdButtonObject.SetActive(false);
            _rewardAdButton.Hide();
            _cost.SetActive(false);
        }
        else
        {
            _rewardAdButtonObject.SetActive(true);
            _rewardAdButton.Show(_skins[_count]);
        }

        _skinView.Show(_skins[_count]);
        _skinView.ChangeText();

        stats.OnStarChanged += (x) => _starsText.text = stats.Stars.ToString();
    }

    public void NextPage()
    {
        if (_count + 1 >= _skins.Length) return;
        _count++;
        _skinView.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.25f);
        _skinView.transform.DOPunchScale(_punchScale, 0.25f).OnComplete(() => _skinView.transform.DOScale(new Vector3(1, 1, 1), 0.15f));
        Show();
    }

    public void PreviousPage()
    {
        if (_count - 1 < 0) return;
        _count--;
        _skinView.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.25f);
        _skinView.transform.DOPunchScale(_punchScale, 0.25f).OnComplete(() => _skinView.transform.DOScale(new Vector3(1, 1, 1), 0.15f));
        Show();
    }
}
