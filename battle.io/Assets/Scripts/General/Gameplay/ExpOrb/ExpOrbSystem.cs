using UnityEngine;

public class ExpOrbSystem : MonoBehaviour
{
    [SerializeField] private Vector2 _innerBorderSize;
    [SerializeField] private Vector2 _outsideBorderSize;

    [SerializeField] private ExpOrb _expOrbPrefab;
    [SerializeField] private int _maxExpOrbAmount;
    [SerializeField] private float _baseExp;
    [SerializeField] private float _maxRandomExp;

    private ExpOrbPool _pool;
    private int _expOrbAmount;
    private int _safePool = 15;

    private void Start()
    {
        _pool = new ExpOrbPool(_maxExpOrbAmount + _safePool);

        for (int i = _expOrbAmount; i < _maxExpOrbAmount - 5; i++)
        {
            SpawnExpOrb();
        }
    }

    private void SpawnExpOrb()
    {
        if (_expOrbAmount == _maxExpOrbAmount) return;
        Vector2 position = new Vector2(Random.Range(_innerBorderSize.x, _outsideBorderSize.x), Random.Range(_innerBorderSize.y, _outsideBorderSize.y));

        if (Physics2D.OverlapCircle(position, 0.3f)) return;
        
        ExpOrb expOrb = _pool.GetExpOrb("ExpOrb");
        
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
