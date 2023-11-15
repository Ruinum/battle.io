using Ruinum.Core.Interfaces;
using UnityEngine;

public class ExpOrbSystem : ISystem
{
    private Transform _player;
    private Vector2 _innerBorderSize;
    private Vector2 _outsideBorderSize;
    private int _maxExpOrbAmount;
    private float _baseExp;
    private float _maxRandomExp;

    private ExpOrbPool _pool;

    private const int DISTANCE = 15;

    public ExpOrbSystem(Vector2 innerBorder, Vector2 outsideBorder, int orbAmount, float baseExp, float randomExp)
    {
        _innerBorderSize = innerBorder;
        _outsideBorderSize = outsideBorder;
        _maxExpOrbAmount = orbAmount;
        _baseExp = baseExp;
        _maxRandomExp = randomExp;
    }

    public void Initialize()
    {
        _pool = Game.Context.ExpOrbPool;
        if (_player != null)
        _player = Game.Context.Player.Transform;
    }

    public void Execute()
    {
        if (Game.Context.ExpOrbs.Count >= _maxExpOrbAmount) return;
        SpawnExpOrb();
    }

    private void SpawnExpOrb()
    {
        Vector2 position = new Vector2();
        if ( _player != null ) position = new Vector2(Random.Range(_player.position.x - DISTANCE, _player.position.x + DISTANCE), Random.Range(_player.position.y - DISTANCE, _player.position.y + DISTANCE));
        else position = new Vector2(Random.Range(_innerBorderSize.x, _outsideBorderSize.x), Random.Range(_innerBorderSize.y , _outsideBorderSize.y));

        if (Physics2D.OverlapCircle(position, 0.3f)) return;
        if (!_pool.TryGetPoolObject(out ExpOrb expOrb)) return;

        float expOrbExpValue = Random.Range(_baseExp, _maxRandomExp);
        expOrb.SetExp(expOrbExpValue);

        expOrb.DelayDestroy(Random.Range(15f, 35f));

        expOrb.Active(position, Quaternion.identity);      
    }
}
