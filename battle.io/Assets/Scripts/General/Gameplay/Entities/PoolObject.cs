using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private string _poolName;

    protected Action OnReturnToPool;

    private Transform _rootPool;    
    public Transform RootPool
    {
        get
        {
            if (_rootPool) return _rootPool;

            var find = GameObject.Find(_poolName);
            _rootPool = find == null ? null : find.transform;
            return _rootPool;
        }
    }

    public void Active(Vector3 position, Quaternion rotation)
    {
        transform.localPosition = position;
        transform.localRotation = rotation;
        gameObject.SetActive(true);
        transform.SetParent(null);
    }

    protected void ReturnToPool()
    {
        Debug.Log("Returning to pool");
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        transform.SetParent(RootPool);

        OnReturnToPool?.Invoke();
        if (!RootPool) Destroy(gameObject);
    }
}