using Ruinum.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnloadSystem : ISystem
{
    private Transform _player;
    private List<Enemy> _enemies;
    private int _unloadDistance;

    public void Initialize()
    {
        _enemies = Game.Context.Enemies;
        _player = Game.Context.Player.Transform;
        Game.Context.OnFinalStage += OnFinalStage;

        _unloadDistance = GameConstants.ENEMY_UNLOAD_DISTANCE;
    }

    public void Execute()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            Enemy enemy = _enemies[i];
            if (_player == null) { _enemies.Clear(); return; }
            if (enemy == null || enemy == default) { _enemies.Remove(enemy); continue; }

            if (Vector2.Distance(_player.position, enemy.transform.position) < _unloadDistance) continue;

            enemy.Unload();
        }
    }

    private void OnFinalStage()
    {
        _unloadDistance += 25;
    }
}
