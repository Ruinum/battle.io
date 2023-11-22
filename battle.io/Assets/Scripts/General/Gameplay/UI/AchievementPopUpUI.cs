using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AchievementPopUpUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _main;
    [SerializeField] private Image _image;

    private void Start()
    {
        _main.alpha = 0f;
    }

    public void ShowAchievement(Achievement achievement)
    {
        _image.sprite = achievement.Icon;
        Debug.Log(achievement.Icon);

        _main.DOFade(1, 0.35f);
        _main.transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0.05f), 0.25f).OnComplete(() => 
        {
            _main.transform.DOScale(1.25f, 4f);
            _main.DOFade(1, 1f).OnComplete(() => { _main.DOFade(0, 2.5f); });
            
        });
    }   
}