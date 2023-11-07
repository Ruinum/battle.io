using Ruinum.Core.Systems;
using System;

public class StatsSystem : System<StatsSystem>
{
    private PlayerStats _stats;
    public Action OnKillEvent;
    public Action<float> OnExpPickedEvent;

    private float _timeSpendedInGame;

    public StatsSystem(PlayerStats stats) => _stats = stats;

    public override void Init()
    {
        OnKillEvent += OnKill;
        OnExpPickedEvent += OnExpPicked;

        Game.Context.OnGameStarted += OnGameStarted;
    }

    public override void Execute() { }

    private void OnGameStarted()
    {
        Game.Context.Player.Level.OnDead += OnGameLose;
        Game.Context.OnFinalStageEnded += OnGameWin;

        OnGameRun();
    }

    private void OnKill()
    {
        _stats.KilledBattlers++;
        _stats.OnStatsChanged?.Invoke();
    }

    private void OnExpPicked(float value)
    {
        _stats.CollectedExp += value;
        _stats.OnStatsChanged?.Invoke();
    }

    private void OnGameWin()
    {
        _stats.GamesWinned++;
        _stats.OnStatsChanged?.Invoke();
    }

    private void OnGameLose(Level level)
    {
        _stats.GamesLosed++;
        _stats.OnStatsChanged?.Invoke();
    }

    private void OnGameRun()
    {
        _timeSpendedInGame = _stats.TimeSpendInGame;

        var gameTimeSpended = TimerSystem.Singleton.StartReverseTimer(999999999f, () => OnGameRun());
        gameTimeSpended.OnTimeChange += (x, y) =>
        {
            _stats.TimeSpendInGame = _timeSpendedInGame + x;
            _stats.OnStatsChanged?.Invoke();
        };
    }
}