using Ruinum.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class GameSystems : ISystem
{
    private List<ISystem> _gameSystems = new List<ISystem>();

    public void Initialize()
    {
        _gameSystems.Add(new ExpOrbSystem(new Vector2(32, 32), new Vector2(-32, -32), 220, 5, 8));
        _gameSystems.Add(new EnemySpawnSystem(18, 10, 10));

        for (int i = 0; i < _gameSystems.Count; i++)
        {
            _gameSystems[i].Initialize();
        }
    }

    public void Execute()
    {
        for (int i = 0; i < _gameSystems.Count; i++)
        {
            _gameSystems[i].Execute();
        }
    }
}
