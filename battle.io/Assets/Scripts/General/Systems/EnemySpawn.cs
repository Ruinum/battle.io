using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject EnemyPref;

    private float _Horiz = 15;
    private float _Vert = 8;
    private Transform Player;
    public int MaxEnemyCount;
    private int _currentEnemyCount = 0;

    private void Start()
    {
        Player = FindObjectOfType<Player>().transform;
        for (int i = 0; i <= MaxEnemyCount; i++)
        {
            Spawn();
        }
    }

    public void EnemyDead()
    {
        if(_currentEnemyCount > 0) _currentEnemyCount--;
        Spawn();
    }

    public void Spawn()
    {
        if (_currentEnemyCount == MaxEnemyCount) return;
        GameObject @object = GameObject.Instantiate(EnemyPref, null);
        float x, y;
        float _dist = 5;
        do
        {
            x = Random.Range(-_Horiz - _dist, _Horiz + _dist);
            y = Random.Range(-_Vert - _dist, _Vert + _dist);
        } while ((x >= -_Horiz && x <= _Horiz) && (y >= -_Vert && y <= _Vert));

        @object.transform.position = Player.position + new Vector3(x, y,0);
        @object.GetComponent<Level>().OnDead += EnemyDead;
        _currentEnemyCount++;
    }


}
