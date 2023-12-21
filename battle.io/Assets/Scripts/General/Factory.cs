using Ruinum.Core.Systems;
using UnityEngine;

public static class Factory<T> where T: Object
{
    public static T CreateObject(string name)
    {
        return UnityEngine.Object.Instantiate(Game.Context.AssetsContext.GetObjectOfType(typeof(GameObject), name)) as T;
    }

    public static T CreateObject(string name, Transform parent)
    {
        return UnityEngine.Object.Instantiate(Game.Context.AssetsContext.GetObjectOfType(typeof(GameObject), name), parent) as T;
    }
}