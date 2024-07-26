using Ruinum.Core.Interfaces;
using UnityEngine;

public class StarSpawnSystem : ISystem
{
    private Transform _player;
    private StarPool _pool;
    
    private Vector2 _border;
    private int _maxStarsAmount;
    private int _distance;
    private float _spawnDelay = 20f;
    private float _delayTime;

    public StarSpawnSystem(int starAmount, Vector2 border)
    {
        _maxStarsAmount = starAmount;
        _border = border;

        _pool = Game.Context.StarPool;
        _distance = GameConstants.STAR_SPAWN_DISTANCE;
    }

    public void InitializeSystem()
    {
        _player = Game.Context.Player.Transform;
        _delayTime = _spawnDelay;
    }

    public void Execute()
    {
        if (Game.Context.Stars.Count >= _maxStarsAmount) return;
        _delayTime -= Time.deltaTime;
        if(_delayTime >= 0 ) return;

        _delayTime = _spawnDelay;
        SpawnStar();
    }

    private void SpawnStar()
    {
        if (_player == null) return;
        float x = Random.Range(_player.position.x - _distance, _player.position.x + _distance);
        float y = Random.Range(_player.position.y - _distance, _player.position.y + _distance);

        Vector2 position = new Vector2(x, y);

        if (Physics2D.OverlapCircle(position, 0.3f)) return;
        if (!_pool.TryGetPoolObject(out Star star)) return;

        star.DelayDestroy(Random.Range(15f, 35f));
        star.Active(position, Quaternion.identity);
    }
}