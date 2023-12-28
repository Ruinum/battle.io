using UnityEngine;

public class ClearSavesButton : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private AchievementsConfig _achievements;
    [SerializeField] private SkinsConfig _skins;

    public void ClearSave()
    {
        _stats.CollectedExp = 0;
        _stats.Stars = 0;
        _stats.KilledBattlers = 0;
        _stats.GamesWinned = 0;
        _stats.GamesLosed = 0;
        _stats.TimeSpendInGame = 0;

        for (int i = 1; i < _achievements.Achievements.Length; i++)
        {
            _achievements.Achievements[i].Unlocked = false;
        }

        for (int i = 1; i < _skins.Skins.Length; i++)
        {
            _skins.Skins[i].Unlocked = false;
        }

        SaveManager.Singleton.Save();
        SaveManager.Singleton.Load();
    }
}
