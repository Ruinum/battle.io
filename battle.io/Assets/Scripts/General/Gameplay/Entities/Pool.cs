using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private GameObject _poolObject;
    private Transform _rootPool;
    private string _poolName;
    private string _poolObjectName;

    private Action _onPoolRemove;

    private readonly Dictionary<string, HashSet<T>> _pool;
    private readonly int _capacityPool;

    public Pool(AssetsContext assetsContext, string poolName, string poolObjectName, int capacityPool)
    {
        _poolObjectName = poolObjectName;
        _poolName = poolName;
        _poolObject = (GameObject)assetsContext.GetObjectOfType(typeof(GameObject), _poolObjectName);
        _pool = new Dictionary<string, HashSet<T>>();
        _capacityPool = capacityPool;

        if (!_rootPool)
        {
            _rootPool = new GameObject(poolName).transform;
            UnityEngine.Object.DontDestroyOnLoad(_rootPool);
        }
    }

    public T GetPoolObject()
    {
        T result = GetPoolObject(GetListElements(_poolObjectName));
        return result;
    }

    private HashSet<T> GetListElements(string type)
    {
        return _pool.ContainsKey(type) ? _pool[type] : _pool[type] = new HashSet<T>();
    }

    private T GetPoolObject(HashSet<T> poolObjects)
    {
        var poolObject = poolObjects.FirstOrDefault(a => !a.gameObject.activeSelf);
        if (poolObject == null)
        {
            for (var i = 0; i < _capacityPool; i++)
            {
                var instantiate = UnityEngine.Object.Instantiate(_poolObject);
                ReturnToPool(instantiate.transform);
                poolObjects.Add(instantiate.GetComponent<T>());
            }

            GetPoolObject(poolObjects);
        }
        poolObject = poolObjects.FirstOrDefault(a => !a.gameObject.activeSelf);
        _onPoolRemove += () => ReturnToPool(poolObject.transform);
        return poolObject;
    }

    private void ReturnToPool(Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.gameObject.SetActive(false);
        transform.SetParent(_rootPool);
    }

    public void InitializePool()
    {
        if (!_rootPool)
        {
            _rootPool = new GameObject(_poolName).transform;
            UnityEngine.Object.DontDestroyOnLoad(_rootPool);
        }
    }

    public void RefreshPool() => _onPoolRemove?.Invoke();

    public void RemovePool()
    {
        UnityEngine.Object.Destroy(_rootPool.gameObject);
    }
}