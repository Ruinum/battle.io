using Ruinum.Utils;
using System;
using UnityEngine;

public class Game
{
    private GameConfig _gameConfig;
    
    public bool GameStarted = false;
    public Action OnGameStarted;
    public Action OnGameEnded;
    
    public Game(GameConfig gameConfig)
    {
        Context = this;
        _gameConfig = gameConfig;

        ExpOrbPool = new ExpOrbPool(_gameConfig.AssetsContext, "Exp Orb Pool", "ExpOrb", _gameConfig.ExpOrbCapacity);
        PopUpPool = new PopUpPool(_gameConfig.AssetsContext, "PopUp Pool", "PopUp", _gameConfig.PopUpCapacity);

        AssetsContext = _gameConfig.AssetsContext;
        LevelStructure = _gameConfig.LevelStructure;
        AbilitiesConfig = _gameConfig.AbilitiesConfig;

        AbilitiesConfig.Initialize();
    }

    public static Game Context { get; private set; }

    public AssetsContext AssetsContext { get; private set; }
    public AbilitiesConfig AbilitiesConfig { get; private set; }
    public LevelStructure LevelStructure { get; private set; }
    public Player Player { get; private set; }
    public Canvas RootCanvas { get; private set; }
    public PlayerUI PlayerUI { get; private set; }
    public ExpOrbPool ExpOrbPool { get; private set; }
    public PopUpPool PopUpPool { get; private set; }

    public void StartGame()
    {
        Player = ObjectUtils.CreateGameObject<Player>(_gameConfig.PlayerPrefab);
        RootCanvas = ObjectUtils.CreateGameObject<Canvas>(_gameConfig.RootCanvas.gameObject);
        PlayerUI = ObjectUtils.CreateGameObject<PlayerUI>(_gameConfig.PlayerUIPrefab, RootCanvas.transform);

        PlayerUI.Initialize(Player);
        ExpOrbPool.InitializePool();
        PopUpPool.InitializePool();

        OnGameStarted?.Invoke();
        GameStarted = true;
    }

    public void EndGame()
    {
        UnityEngine.Object.Destroy(Player);
        UnityEngine.Object.Destroy(RootCanvas);
        UnityEngine.Object.Destroy(PlayerUI);
        
        ExpOrbPool.RefreshPool();
        PopUpPool.RefreshPool();

        OnGameEnded?.Invoke();
        GameStarted = false;
    }
}
