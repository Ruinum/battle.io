using Ruinum.Core.Interfaces;
using UnityEngine;

public class EnemySpawnSystem : ISystem
{
    [InjectAsset("Enemy")] private GameObject _enemyPrefab;

    private Player _player;
    private Transform _point;
    private float _horizontal;
    private float _vertical;    
    private int _maxEnemyCount;
    private int _currentEnemyCount = 0;

    public EnemySpawnSystem(float horizontalSize, float verticalSize, int enemyCount)
    {
        _horizontal = horizontalSize;
        _vertical = verticalSize;
        _maxEnemyCount = enemyCount;

        Game.Context.AssetsContext.Inject(this);
    }
    
    public void Initialize()
    {
        if (Game.Context.Player != null) { _player = Game.Context.Player; _point = _player.transform; }
        for (int i = 0; i <= _maxEnemyCount; i++)
        {
            Spawn();
        }
    }

    public void Execute() { }

    public void EnemyDead()
    {
        if(_currentEnemyCount > 0) _currentEnemyCount--;
        Spawn();
    }

    public void Spawn()
    {
        if (_currentEnemyCount == _maxEnemyCount) return;
        if (_player == null || _player == default)
        {
            if (Game.Context.Player != null) { _player = Game.Context.Player; _point = _player.transform; }
            return;
        }

        GameObject createdEnemy = GameObject.Instantiate(_enemyPrefab, null);
        float x, y;
        float _dist = 5;
        do
        {
            x = Random.Range(-_horizontal - _dist, _horizontal + _dist);
            y = Random.Range(-_vertical - _dist, _vertical + _dist);
        } while ((x >= -_horizontal && x <= _horizontal) && (y >= -_vertical && y <= _vertical));

        createdEnemy.transform.position = _point.position + new Vector3(x, y, 0);
        
        var level = createdEnemy.GetComponent<Level>();
        level.OnDead += EnemyDead;

        _currentEnemyCount++;
    }

    public void SetPlayer(Transform transform)
    {
        _point = transform;
    }
}