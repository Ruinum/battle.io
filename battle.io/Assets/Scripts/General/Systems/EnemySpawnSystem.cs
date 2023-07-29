using Ruinum.Core.Interfaces;
using UnityEngine;

public class EnemySpawnSystem : ISystem
{
    [InjectAsset("Enemy")] private GameObject EnemyPrefab;
    
    private Transform Player;
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
        if (Game.Context.Player != null) Player = Game.Context.Player.transform;
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
        if (Player == null || Player == default)
        {
            if (Game.Context.Player != null) Player = Game.Context.Player.transform;
            return;
        }

        GameObject createdEnemy = GameObject.Instantiate(EnemyPrefab, null);
        float x, y;
        float _dist = 5;
        do
        {
            x = Random.Range(-_horizontal - _dist, _horizontal + _dist);
            y = Random.Range(-_vertical - _dist, _vertical + _dist);
        } while ((x >= -_horizontal && x <= _horizontal) && (y >= -_vertical && y <= _vertical));

        createdEnemy.transform.position = Player.position + new Vector3(x, y,0);
        createdEnemy.GetComponent<Level>().OnDead += EnemyDead;
        _currentEnemyCount++;
    }
}