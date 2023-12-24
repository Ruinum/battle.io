using UnityEngine;
using TMPro;

public class AchievementWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private AchievementsConfig _achievementsConfig;    
    [SerializeField] private AchievementUI[] _achievementViews;

    private Achievement[] _achievements;

    private int _page = 1;
    private int _count = 0;

    private const int ACHIEVEMENT_VIEW_COUNT = 12;

    private void Awake()
    {
        _achievements = _achievementsConfig.Achievements;
    }

    public void Show()
    {
        _count = 0;
        _name.text = "";
        _description.text = "";

        for (int i = 0; i < ACHIEVEMENT_VIEW_COUNT; i++)
        {
            _achievementViews[i].gameObject.SetActive(false);
        }

        for (int i = 0 + ((_page - 1) * ACHIEVEMENT_VIEW_COUNT); i < _achievements.Length; i++)
        {
            _achievementViews[_count].gameObject.SetActive(true);
            _achievementViews[_count].Show(_achievements[i]);

            _count++;
            if (_count >= ACHIEVEMENT_VIEW_COUNT) break;
        }
    }

    public void NextPage()
    {
        if ((_achievements.Length - (_page) * ACHIEVEMENT_VIEW_COUNT) >= 1) _page++;
        _count = 0;
        Show();
    }

    public void PreviousPage()
    {
        if (_page - 1 >= 1) _page--;
        _count = 0;
        Show();
    }
}