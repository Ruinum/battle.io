using System;
using UnityEngine;

public class UniqueObject : ScriptableObject
{
    private string _guid;
    public string GUID 
    { 
        get 
        {
            if (_guid == null) _guid = Guid.NewGuid().ToString();
            return _guid;
        } 
    }
}
