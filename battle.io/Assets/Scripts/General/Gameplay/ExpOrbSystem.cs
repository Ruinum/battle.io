using Ruinum.Core;
using UnityEngine;

public class ExpOrbSystem : Executable
{
    [SerializeField] private Vector2 _innerBorderSize;
    [SerializeField] private Vector2 _outsideBorderSize;

    [SerializeField] private ExpOrb _expOrbPrefab;
    [SerializeField] private int _maxExpOrbAmount;
    [SerializeField] private float _baseExp;
    [SerializeField] private float _maxRandomExp;    

    private int _expOrbAmount;

    public override void Execute()
    {
        for (int i = _expOrbAmount; i < _maxExpOrbAmount; i++)
        {
            SpawnExpOrb();
        }
    }

    private void SpawnExpOrb()
    {
        Vector2 position = new Vector2(Random.Range(_innerBorderSize.x, _outsideBorderSize.x), Random.Range(_innerBorderSize.y, _outsideBorderSize.y));

        if (Physics2D.OverlapCircle(position, 0.3f)) return;
        
        ExpOrb createdExpOrb = Instantiate(_expOrbPrefab, position, Quaternion.identity);        
        createdExpOrb.OnInteract += RefreshAmount;
        _expOrbAmount++;
    }

    private void RefreshAmount()
    {
        _expOrbAmount--;
    }
}
