using Ruinum.Core.Interfaces;
using UnityEngine;

public class EnemySpawnSystem : ISystem
{
    [InjectAsset("Enemy")] private GameObject _enemyPrefab;
    [InjectAsset("ExpOrb")] private GameObject _expOrb;

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
        _player = Game.Context.Player;
        if (_player == null) return;
        _point = _player.transform;

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
        if (_point == null) return; 

        GameObject createdEnemy = GameObject.Instantiate(_enemyPrefab);
        var enemyView = createdEnemy.GetComponent<EnemyView>();
        
        enemyView.Initialize();
        enemyView.Show();

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

        var expOrb = Game.Context.ExpOrbPool.GetPoolObject();
        expOrb.transform.position = createdEnemy.transform.position;
        expOrb.SetExp(UnityEngine.Random.Range(50f, 450f));

        _currentEnemyCount++;
    }

    public void SetPlayer(Transform transform)
    {
        _point = transform;
        _player = Game.Context.Player;
        for (int i = 0; i <= _maxEnemyCount; i++)
        {
            Spawn();
        }
    }
}