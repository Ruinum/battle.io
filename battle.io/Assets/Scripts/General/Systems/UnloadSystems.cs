using Ruinum.Core.Interfaces;
using System.Collections.Generic;

public class UnloadSystems : ISystem
{
    private List<ISystem> _unloadSystems = new List<ISystem>();

    public void Initialize()
    {
        _unloadSystems.Add(new EnemyUnloadSystem());
        _unloadSystems.Add(new ExpOrbUnloadSystem());

        for (int i = 0; i < _unloadSystems.Count; i++)
        {
            _unloadSystems[i].Initialize();
        }
    }

    public void Execute()
    {
        for (int i = 0; i < _unloadSystems.Count; i++)
        {
            _unloadSystems[i].Execute();
        }
    }
}