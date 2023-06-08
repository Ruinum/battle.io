using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class ExpOrbPool
{
    private AssetsContext _context;
    private readonly Dictionary<string, HashSet<ExpOrb>> _expOrbPool;
    private readonly int _capacityPool;
    private Transform _rootPool;

    public ExpOrbPool(int capacityPool)
    {
        _expOrbPool = new Dictionary<string, HashSet<ExpOrb>>();
        _capacityPool = capacityPool;
        _context = Resources.Load<AssetsContext>("Content/Data/AssetsContext");

        if (!_rootPool)
        {
            _rootPool = new GameObject("Exp Orb Pool").transform;
        }
    }

    public ExpOrb GetExpOrb(string type)
    {
        ExpOrb result = GetExpOrbObject(GetListElements(type));
        return result;
    }

    private HashSet<ExpOrb> GetListElements(string type)
    {
        return _expOrbPool.ContainsKey(type) ? _expOrbPool[type] : _expOrbPool[type] = new HashSet<ExpOrb>();
    }

    private ExpOrb GetExpOrbObject(HashSet<ExpOrb> expOrbs)
    {
        var expOrb = expOrbs.FirstOrDefault(a => !a.gameObject.activeSelf);
        if (expOrb == null)
        {
            GameObject findedObject = (GameObject)_context.GetObjectOfType(typeof(GameObject), "ExpOrb");
            
            for (var i = 0; i < _capacityPool; i++)
            {
                var instantiate = Object.Instantiate(findedObject);
                ReturnToPool(instantiate.transform);
                expOrbs.Add(instantiate.GetComponent<ExpOrb>());
            }
        
        GetExpOrbObject(expOrbs);
        }
        expOrb = expOrbs.FirstOrDefault(a => !a.gameObject.activeSelf);
        return expOrb;
    }

    private void ReturnToPool(Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.gameObject.SetActive(false);
        transform.SetParent(_rootPool);
    }

    public void RemovePool()
    {
        Object.Destroy(_rootPool.gameObject);
    }
}
