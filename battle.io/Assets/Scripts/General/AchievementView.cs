using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Ruinum.Utils;

public class AchievementView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _blockImage;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private AudioConfig _audioConfig;

    private Achievement _achievement;
    private Vector3 _punchScale = new Vector3(0.15f, 0.15f, 0.15f);

    public void Initialize()
    {
        Hide();
    }

    public void Show(Achievement achievement)
    {
        Hide();
        _image.sprite = achievement.Icon;
        _achievement = achievement;
        
        if (achievement.Unlocked) _blockImage.SetActive(false);
    }

    public void Hide() 
    {
        _blockImage.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOPunchScale(_punchScale, 0.25f).OnComplete(() => transform.DOScale(new Vector3(1, 1, 1), 0.15f));
        AudioUtils.PlayAudio(_audioConfig, transform.position);
        
        _name.text = _achievement.Name;
        _description.text = _achievement.Description;
    }
}
