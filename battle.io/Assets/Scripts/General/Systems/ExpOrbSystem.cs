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
    }

    public void Execute()
    {
        if (_expOrbAmount >= _maxExpOrbAmount) return;
        SpawnExpOrb();
    }

    private void SpawnExpOrb()
    {
        Vector2 position = new Vector2(Random.Range(_innerBorderSize.x, _outsideBorderSize.x), Random.Range(_innerBorderSize.y, _outsideBorderSize.y));

        if (Physics2D.OverlapCircle(position, 0.3f)) return;
        if (!_pool.TryGetPoolObject(out ExpOrb expOrb)) return;

        float expOrbExpValue = Random.Range(_baseExp, _maxRandomExp);
        expOrb.SetExp(expOrbExpValue);

        expOrb.DelayDestroy(Random.Range(15f, 35f));

        expOrb.Active(position, Quaternion.identity);
        expOrb.OnInteract += RefreshAmount;
        
        _expOrbAmount++;
    }

    private void RefreshAmount()
    {
        _expOrbAmount--;
    }
}
