using Ruinum.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : ISystem
{
    [InjectAsset("Enemy")] private GameObject _enemyPrefab;

    private Player _player;
    private Transform _point;
    private ExpOrbPool _pool;
    private List<Enemy> _enemies;

    private float _horizontal;
    private float _vertical;    
    private int _maxEnemyCount;
    private int _currentEnemyCount = 0;

    private const float MAX_LEVEL_EXP = 450f;

    public EnemySpawnSystem(float horizontalSize, float verticalSize, int enemyCount)
    {
        _horizontal = horizontalSize;
        _vertical = verticalSize;
        _maxEnemyCount = enemyCount;

        _pool = Game.Context.ExpOrbHitImpactPool;
        _enemies = new List<Enemy>();

        Game.Context.AssetsContext.Inject(this);
    }
    
    public void Initialize()
    {
        Game.Context.OnFinalStage += OnFinalStage;

        _player = Game.Context.Player;
        if (_player == null) return;
        _point = _player.transform;

        for (int i = 0; i <= _maxEnemyCount; i++)
        {
            Spawn();
        }
    }

    public void Execute() { }

    public void EnemyDead(Level level)
    {
        if(_currentEnemyCount > 0) _currentEnemyCount--;
        if (Game.Context.FinalStage && _currentEnemyCount == 0 && _enemies.Count <= 1) Game.Context.FinalGameEnded();
        
        Spawn();
        _enemies.Remove(level.GetComponent<Enemy>());
    }

    public void Spawn()
    {
        if (_currentEnemyCount >= _maxEnemyCount) return;
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
        _enemies.Add(level.gameObject.GetComponent<Enemy>());
        level.OnDead += EnemyDead;

        if (_player == null) { _player = Game.Context.Player; return; }
        if (!_pool.TryGetPoolObject(out ExpOrb expOrb)) return;

        float randomValue = UnityEngine.Random.Range(80, 250f) * _player.Level.PlayerLevel * 0.4f;
        expOrb.Active(createdEnemy.transform.position, Quaternion.identity);
        expOrb.SetExp(Mathf.Min(MAX_LEVEL_EXP, randomValue));

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

    private void OnFinalStage()
    {
        _maxEnemyCount = 0;

        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].FinalStage();
        }
    }
}