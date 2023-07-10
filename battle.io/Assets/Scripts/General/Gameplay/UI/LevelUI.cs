using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _levelText;
    
    private float _maxAmount;

    private void Start()
    {
        _level.OnExpChange += UpdateExp;
        _level.OnLevelChange += UpdateLevel;
        _level.OnNextExpChange += UpdateExpAmount;
    }

    private void UpdateExp(float currentValue)
    {
        _image.fillAmount = currentValue / _maxAmount;
    }

    private void UpdateLevel(int currentLevel)
    {
        _levelText.text = currentLevel.ToString();
    }

    private void UpdateExpAmount(float expAmount) => _maxAmount = expAmount;
}
