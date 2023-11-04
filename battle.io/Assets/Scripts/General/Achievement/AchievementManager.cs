using Ruinum.Core.Systems;
using System;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    public static AchievementManager Singleton { get; private set; }

    public Action OnKillEvent;
    public Action OnGameWinEvent;
    public Action OnGameLoseEvent;
    public Action<float> OnExpPickedEvent;

    private float _timeSpendedInGame;

    public void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        OnKillEvent += OnKill;
        OnExpPickedEvent += OnExpPicked;

        Game.Context.OnGameStarted += OnGameStarted;       
    }

    private void OnGameStarted()
    {
        Game.Context.Player.Level.OnDead += OnGameLose;
        Game.Context.OnFinalStageEnded += OnGameWin;

        OnGameRun();
    }

    private void OnKill()
    {
        _stats.KilledBattlers++;
    }

    private void OnExpPicked(float value)
    {
        _stats.CollectedExp += value;
    }

    private void OnGameWin()
    {
        _stats.GamesWinned++;
    }

    private void OnGameLose(Level level)
    {
        _stats.GamesLosed++;
    }

    private void OnGameRun()
    {
        _timeSpendedInGame = _stats.TimeSpendInGame;

        var gameTimeSpended = TimerSystem.Singleton.StartReverseTimer(999999999f, () => OnGameRun());
        gameTimeSpended.OnTimeChange += (x, y) => _stats.TimeSpendInGame = _timeSpendedInGame + x;
    }
}