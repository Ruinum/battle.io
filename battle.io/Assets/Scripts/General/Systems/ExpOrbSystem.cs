using Ruinum.Core.Interfaces;
using UnityEngine;

public class ExpOrbSystem : ISystem
{   
    private Vector2 _innerBorderSize;
    private Vector2 _outsideBorderSize;
    private int _maxExpOrbAmount;
    private float _baseExp;
    private float _maxRandomExp;

    private ExpOrbPool _pool;
    private int _expOrbAmount;
    private int _safeZone = 35;

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

        for (int i = _expOrbAmount; i < _maxExpOrbAmount - _safeZone; i++)
        {
            SpawnExpOrb();
        }
    }

    public void Execute() { }

    private void SpawnExpOrb()
    {
        if (_expOrbAmount == _maxExpOrbAmount) return;
        Vector2 position = new Vector2(Random.Range(_innerBorderSize.x, _outsideBorderSize.x), Random.Range(_innerBorderSize.y, _outsideBorderSize.y));

        if (Physics2D.OverlapCircle(position, 0.3f)) return;
        
        ExpOrb expOrb = _pool.GetPoolObject();

        float expOrbExpValue = Random.Range(_baseExp, _maxRandomExp);
        expOrb.SetExp(expOrbExpValue);

        expOrb.Active(position, Quaternion.identity);
        expOrb.OnInteract += RefreshAmount;
        
        _expOrbAmount++;
    }

    private void RefreshAmount()
    {
        _expOrbAmount--;
        SpawnExpOrb();
    }
}
