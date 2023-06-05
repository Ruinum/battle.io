using Ruinum.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    
    private Level _level;
    private float _maxFillAmount;

    private void Start()
    {
        _level = this.GetComponentInObject<Level>();
        
        _level.OnExpChange  += OnExpChange;
        _level.OnLevelUp += OnLevelUp;
        _level.OnLevelChange += OnLevelChange;
    }

    private void OnExpChange(float currentExp)
    {
        _image.fillAmount = currentExp / _maxFillAmount;
    }

    private void OnLevelUp(float neededExpForLevelUp)
    {
        _maxFillAmount = neededExpForLevelUp;
    }

    private void OnLevelChange(int currentLevel)
    {
        _text.text = currentLevel.ToString();
    }
}