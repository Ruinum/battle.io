using Ruinum.Core.Interfaces;
using System.Collections.Generic;

public class AchievementSystem : ISystem
{
    private PlayerStats _stats;

    private List<Achievement> _killAchievements;
    private List<Achievement> _winAchievements;
    private List<Achievement> _loseAchievements;
    private List<Achievement> _expAchievements;
    private List<Achievement> _timeAchievements;

    private int _killAchievementCount = 0;
    private int _kills = 1;

    private int _winAchievementCount = 0;
    private int _wins = 1;

    private int _loseAchievementCount = 0;
    private int _loses = 1;

    private int _expAchievementCount = 0;
    private int _exp = 1000;

    private int _timeAchievementCount = 0;
    private int _timeSpended = 1800;


    public AchievementSystem(GameConfig config)
    {
        _stats = config.PlayerStats;

        _killAchievements = config.KillAchievement;
        _winAchievements = config.WinAchievements;
        _loseAchievements = config.LoseAchievements;
        _expAchievements = config.ExpAchievements;
        _timeAchievements = config.TimeAchievements;
    }

    public void Initialize()
    {
        _stats.OnKillsChanged += CheckKillAchievements;
        _stats.OnWinsChanged += CheckWinAchievements;
        _stats.OnLosesChanged += CheckLoseAchievements;
        _stats.OnTimeSpendedChanged += CheckGameTimeAchievements;
        _stats.OnExpChanged += CheckExpAchievements;
    }

    public void Execute() { }

    private void CheckKillAchievements(int kills)
    {
        if (kills == _kills) NextKillAchievemnt();
    }

    private void CheckExpAchievements(float exp)
    {
        if (exp == _exp) NextExpAchievement();
    }

    private void CheckWinAchievements(int wins)
    {
        if (wins == _wins) NextWinAchievemnt();
    }

    private void CheckLoseAchievements(int loses)
    {
        if (loses == _loses) NextLoseAchievemnt();
    }

    private void CheckGameTimeAchievements(float timeSpended)
    {
        if (timeSpended == _timeSpended) NextGameTimeAchievement();
    }

    private void NextKillAchievemnt()
    {
        _killAchievementCount++;
        switch (_killAchievementCount)
        {
            case 1:
                _kills = 10;
                break;
            case 2:
                _kills = 50;
                break;
            case 3:
                _kills = 200;
                break;

            default:
                break;
        }

        UnlockNextAchievement(_killAchievements);
    }

    private void NextWinAchievemnt()
    {
        _winAchievementCount++;
        switch (_winAchievementCount)
        {
            case 1:
                _wins = 10;
                break;

            default:
                break;
        }

        UnlockNextAchievement(_winAchievements);
    }

    private void NextLoseAchievemnt()
    {
        _loseAchievementCount++;
        switch (_loseAchievementCount)
        {
            case 1:
                _loses = 10;
                break;

            default:
                break;
        }

        UnlockNextAchievement(_loseAchievements);
    }

    private void NextExpAchievement()
    {
        _expAchievementCount++;
        switch (_expAchievementCount)
        {
            case 1:
                _exp = 10000;
                break;
            case 2:
                _exp = 100000;
                break;

            default:
                break;
        }

        UnlockNextAchievement(_expAchievements);
    }

    private void NextGameTimeAchievement()
    {
        _timeAchievementCount++;
        switch (_timeAchievementCount)
        {
            case 1:
                _timeSpended = 3600;
                break;
            case 2:
                _timeSpended = 7200;
                break;

            default:
                break;
        }

        UnlockNextAchievement(_timeAchievements);
    }

    private void UnlockNextAchievement(List<Achievement> list)
    {
        list[0].Unlocked = true;
        list.Remove(list[0]);
    }
}