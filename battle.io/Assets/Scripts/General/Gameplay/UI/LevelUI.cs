using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Transform _parent;
    [SerializeField] private TMP_Text _levelText;

    private Level _level;
    private float _maxAmount = 100f;
    private float _fillDuration = 0.5f;
    private float _punchDuration = 0.25f;
    private Vector3 _punchScale = new Vector3(0.025f, 0.025f, 0.025f);

    public void Initialize(Level level)
    {
        _level = level;

        _level.OnExpChange += UpdateExp;
        _level.OnLevelChange += UpdateLevel;
        _level.OnNextExpChange += UpdateExpAmount;
    }

    private void UpdateExp(float currentValue)
    {
        _image.DOFillAmount(currentValue / _maxAmount, _fillDuration);
        _parent.DOPunchScale(_punchScale, _punchDuration).OnComplete(() => _parent.DOScale(1, _punchDuration));
    }

    private void UpdateLevel(int currentLevel)
    {
        _levelText.text = currentLevel.ToString();
        _levelText.transform.DOPunchScale(_punchScale, _punchDuration);
    }

    private void UpdateExpAmount(float expAmount) => _maxAmount = expAmount;
}
