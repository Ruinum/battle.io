using Ruinum.Core.Interfaces;
using UnityEngine;

public class ExpOrbSystem : ISystem
{   
    //TODO: Make injecting from assets context (check pool and delete all not nessesary fields)
    private ExpOrb _expOrbPrefab;
    private Vector2 _innerBorderSize;
    private Vector2 _outsideBorderSize;
    private int _maxExpOrbAmount;
    private float _baseExp;
    private float _maxRandomExp;

    private ExpOrbPool _pool;
    private int _expOrbAmount;
    private int _safePool = 15;

    public ExpOrbSystem(Vector2 innerBorder, Vector2 outsideBorder, int orbAmount, float baseExp, float randomExp)
    {
        _innerBorderSize = innerBorder;
        _outsideBorderSize = outsideBorder;
        _maxExpOrbAmount = orbAmount;
        _baseExp = baseExp;
        _maxRandomExp = randomExp;

        Game.Context.AssetsContext.Inject(this);
    }

    public void Initialize()
    {
        _pool = new ExpOrbPool(_maxExpOrbAmount + _safePool);

        for (int i = _expOrbAmount; i < _maxExpOrbAmount - 5; i++)
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
        
        ExpOrb expOrb = _pool.GetExpOrb("ExpOrb");

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
