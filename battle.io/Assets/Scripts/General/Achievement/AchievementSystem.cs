using Ruinum.Core.Interfaces;

public class AchievementSystem : ISystem
{
    private PlayerStats _stats;

    public AchievementSystem(PlayerStats stats)
    {
        _stats = stats;
    }

    public void Initialize()
    {
        _stats.OnStatsChanged += OnStatsChange;
    }

    public void Execute() { }

    private void OnStatsChange()
    {
        CheckKillAchievements();
        CheckExpAchievements();
        CheckWinAchievements();
        CheckLoseAchievements();
        CheckGameTimeAchievements();
    }

    private void CheckKillAchievements()
    {

    }

    private void CheckExpAchievements()
    {

    }

    private void CheckWinAchievements()
    {

    }

    private void CheckLoseAchievements()
    {

    }

    private void CheckGameTimeAchievements()
    {

    }
}