using Ruinum.Core.Interfaces;
using UnityEngine;

public class EnemySpawnSystem : ISystem
{
    [InjectAsset("Enemy")] private GameObject _enemyPrefab;

    private Game _context;
    private IPlayer _player;
    private Vector3 _point;
    private ExpOrbPool _pool;

    private int _maxEnemyCount;
    private float _maxLevelExp;
    private int _finalGameEnemies = 20;
    private float _minimalSpawnDistance = 5f;
    private float _additionalSpawnDistance = 5f;

    public EnemySpawnSystem(int enemyCount)
    {
        _maxEnemyCount = enemyCount;
        _context = Game.Context;

        _pool = _context.ExpOrbHitImpactPool;

        _maxLevelExp = GameConstants.MAX_ENEMY_LEVEL_EXP;
        _context.AssetsContext.Inject(this);
    }
    
    public void InitializeSystem()
    {
        _context.OnFinalStage += OnFinalStage;

        _player = _context.Player;
        if (_player == null) { _point = new Vector3(0, 0, 0); return; }
        
        if(_player.IsDestroyed) return; 
        _point = _player.Transform.position;

        for (int i = 0; i <= _maxEnemyCount; i++)
        {
            Spawn();
        }
    }

    public void Execute() { }

    public void EnemyDead(Level level)
    {
        if (_context.FinalStage && _context.Enemies.Count <= 1) _context.FinalGameEnded();
        
        Spawn();
        _context.Enemies.Remove(level.GetComponent<Enemy>());
    }

    public void Spawn()
    {
        if (_context.Enemies.Count - 1 >= _maxEnemyCount) return;
        if (_point == null) return; 

        GameObject createdEnemy = GameObject.Instantiate(_enemyPrefab);
        var enemyView = createdEnemy.GetComponent<EnemyView>();

        enemyView.Initialize();
        enemyView.Show();

        if (_context.Player != null && !_context.Player.IsDestroyed) _point = _context.Player.Transform.position;

        var position = GetRandomSpawnPosition();
        if (Physics2D.OverlapCircle(position, 0.3f)) return;

        createdEnemy.transform.position = _point + position;
        
        var level = createdEnemy.GetComponent<Level>();
        var enemy = level.gameObject.GetComponent<Enemy>();
        enemy.Initialize();

        _context.Enemies.Add(enemy);

        level.OnDead += EnemyDead;

        if (_player == null) { _player = _context.Player; return; }
        if (!_pool.TryGetPoolObject(out ExpOrb expOrb)) return;

        if (_player.Level == null) return;
        float randomValue = Random.Range(80, 250f) * _player.Level.PlayerLevel * 0.4f;
        expOrb.Active(createdEnemy.transform.position, Quaternion.identity);
        expOrb.SetExp(Mathf.Min(_maxLevelExp, randomValue));
    }

    public void SetPlayer(IPlayer player)
    {
        _point = player.Transform.position;
        _player = player;

        for (int i = 0; i <= _maxEnemyCount; i++)
        {
            Spawn();
        }
    }

    private void OnFinalStage()
    {
        _additionalSpawnDistance *= 2;
        _maxEnemyCount += _finalGameEnemies;

        for (int i = 0; i < _finalGameEnemies; i++)
        {
            Spawn();
        }

        var enemies = _context.Enemies;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].FinalStage();
        }

        _maxEnemyCount = 0;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 result = new Vector3();

        int randomizer = Random.Range(0, 100);

        if (randomizer < 51) result.x = Random.Range(-_minimalSpawnDistance, -_minimalSpawnDistance - _additionalSpawnDistance);
        else result.x = Random.Range(_minimalSpawnDistance, _minimalSpawnDistance + _additionalSpawnDistance);

        randomizer = Random.Range(0, 100);

        if (randomizer < 51) result.y = Random.Range(-_minimalSpawnDistance, -_minimalSpawnDistance - _additionalSpawnDistance);
        else result.y = Random.Range(_minimalSpawnDistance, _minimalSpawnDistance + _additionalSpawnDistance);

        return result;
    }
}