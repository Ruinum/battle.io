using UnityEngine;
using System.Collections;

class MenuGameplay : MonoBehaviour
{
    [SerializeField] private GameObject _fakePlayer;
    private ExpOrbSystem _expOrbSystem;
    private EnemySpawnSystem _enemySpawnSystem;
    private bool _initialize;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        Initialize();
    }

    private void Initialize()
    {
        _expOrbSystem = new ExpOrbSystem(new Vector2(32, 32), new Vector2(-32, -32), 250, 5, 8);
        _enemySpawnSystem = new EnemySpawnSystem(15);
        var fakePlayer = Object.Instantiate(_fakePlayer).GetComponent<IPlayer>();
        DontDestroyOnLoad(fakePlayer.Transform);
        _enemySpawnSystem.SetPlayer(fakePlayer);
        Game.Context.Player = fakePlayer;

        _expOrbSystem.Initialize();
        _enemySpawnSystem.Initialize();
        _initialize = true;
    }

    public void Update()
    {
        if (!_initialize) return;
        _expOrbSystem.Execute();
        _enemySpawnSystem.Execute();
    }
}