using DG.Tweening;
using TMPro;
using UnityEngine;

public class SkinWindow : MonoBehaviour
{
    [SerializeField] private GameObject _cost;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _starsText;
    [SerializeField] private SkinUnlockableUI _skinView;
    [SerializeField] private Skin[] _skins;
    
    private int _count = 0;

    public void Show()
    {
        _costText.text = _skins[_count].Cost.ToString();
        _starsText.text = Game.Context.Stats.Stars.ToString();
        _cost.SetActive(true);

        if (_skins[_count].Unlocked) _cost.SetActive(false);

        _skinView.Show(_skins[_count]);
        _skinView.ChangeText();
    }

    public void NextPage()
    {
        if (_count + 1 >= _skins.Length) return;
        _count++;
        _skinView.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.25f);
        Show();
    }

    public void PreviousPage()
    {
        if (_count - 1 < 0) return;
        _count--;
        _skinView.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.25f);
        Show();
    }
}
