using Ruinum.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbUnloadSystem : ISystem
{
    private Transform _player;
    private List<ExpOrb> _expOrbs;
    private int _unloadDistance;

    public void Initialize()
    {
        _expOrbs = Game.Context.ExpOrbs;
        _player = Game.Context.Player.Transform;

        _unloadDistance = GameConstants.EXP_ORB_UNLOAD_DISTANCE;
    }

    public void Execute()
    {
        for (int i = 0; i < _expOrbs.Count; i++)
        {
            ExpOrb expOrb = _expOrbs[i];

            if (_player == null) { _expOrbs.Clear(); return; }
            if (!expOrb.isActiveAndEnabled) continue;
            if (Vector2.Distance(_player.position, expOrb.transform.position) < _unloadDistance) continue;

            Game.Context.ExpOrbs.Remove(expOrb);
            expOrb.ReturnToPool();
        }
    }
}