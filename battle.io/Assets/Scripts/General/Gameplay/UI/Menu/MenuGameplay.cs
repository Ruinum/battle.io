using UnityEngine;
using System.Collections;

class MenuGameplay : MonoBehaviour
{
    [SerializeField] private GameObject _fakePlayer;
    private ExpOrbSpawnSystem _expOrbSystem;
    private EnemySpawnSystem _enemySpawnSystem;
    private bool _initialize;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        Initialize();
    }

    private void Initialize()
    {
        _expOrbSystem = new ExpOrbSpawnSystem(new Vector2(32, 32), new Vector2(-32, -32), 250, 5, 8);
        _enemySpawnSystem = new EnemySpawnSystem(15);
        var fakePlayer = Object.Instantiate(_fakePlayer).GetComponent<IPlayer>();
        DontDestroyOnLoad(fakePlayer.Transform);
        _enemySpawnSystem.SetPlayer(fakePlayer);
        Game.Context.Player = fakePlayer;

        _expOrbSystem.InitializeSystem();
        _enemySpawnSystem.InitializeSystem();
        _initialize = true;
    }

    public void Update()
    {
        if (!_initialize) return;
        _expOrbSystem.Execute();
        _enemySpawnSystem.Execute();
    }
}