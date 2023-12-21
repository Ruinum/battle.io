using Ruinum.Core.Interfaces;
using System.Collections.Generic;

public class UnloadSystems : ISystem
{
    private List<ISystem> _unloadSystems = new List<ISystem>();

    public void InitializeSystem()
    {
        _unloadSystems.Add(new EnemyUnloadSystem());
        _unloadSystems.Add(new ExpOrbUnloadSystem());

        for (int i = 0; i < _unloadSystems.Count; i++)
        {
            _unloadSystems[i].InitializeSystem();
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