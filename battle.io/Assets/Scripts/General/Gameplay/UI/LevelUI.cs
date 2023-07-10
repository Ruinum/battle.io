using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private Image _image;
    [SerializeField] private Transform _parent;
    [SerializeField] private TMP_Text _levelText;
    
    private float _maxAmount;
    private float _fillDuration = 0.5f;
    private float _shakeDuration = 0.25f;
    private Vector3 _shakeScale = new Vector3(0.025f, 0.025f, 0.025f);

    private void Start()
    {
        _level.OnExpChange += UpdateExp;
        _level.OnLevelChange += UpdateLevel;
        _level.OnNextExpChange += UpdateExpAmount;
    }

    private void UpdateExp(float currentValue)
    {
        _image.DOFillAmount(currentValue / _maxAmount, _fillDuration);
        _parent.DOPunchScale(_shakeScale, _shakeDuration);
    }

    private void UpdateLevel(int currentLevel)
    {
        _levelText.text = currentLevel.ToString();
    }

    private void UpdateExpAmount(float expAmount) => _maxAmount = expAmount;
}
