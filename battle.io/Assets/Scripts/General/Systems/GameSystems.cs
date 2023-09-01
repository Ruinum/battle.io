using Ruinum.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : ISystem
{
    private List<ISystem> _gameSystems = new List<ISystem>();
    private bool _initialized;

    public void Initialize()
    {
        Game.Context.OnGameStarted += StartGame;
        Game.Context.OnGameEnded += EndGame;
    }

    public void Execute()
    {
        if (!_initialized) return;

        for (int i = 0; i < _gameSystems.Count; i++)
        {
            _gameSystems[i].Execute();
        }
    }

    private void StartGame()
    {
        _gameSystems.Add(new ExpOrbSystem(new Vector2(32, 32), new Vector2(-32, -32), 220, 5, 8));
        _gameSystems.Add(new EnemySpawnSystem(18, 10, 45));

        for (int i = 0; i < _gameSystems.Count; i++)
        {
            _gameSystems[i].Initialize();
        }

        _initialized = true;
    }


    private void EndGame()
    {
        _gameSystems.Clear();
        _initialized = false;
    }
}
