using Ruinum.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : ISystem
{
    private List<ISystem> _gameSystems = new List<ISystem>();
    private bool _initialized;

    public void InitializeSystem()
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
        _gameSystems.Add(new ExpOrbSpawnSystem(new Vector2(32, 32), new Vector2(-32, -32), 100, 5, 8));
        _gameSystems.Add(new EnemySpawnSystem(20));
        _gameSystems.Add(new StarSpawnSystem(5));
        _gameSystems.Add(new UnloadSystems());

        for (int i = 0; i < _gameSystems.Count; i++)
        {
            _gameSystems[i].InitializeSystem();
        }

        _initialized = true;
    }


    private void EndGame()
    {
        _gameSystems.Clear();
        _initialized = false;
    }
}
