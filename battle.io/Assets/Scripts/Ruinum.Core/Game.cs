using Ruinum.Utils;
using UnityEngine;

public class Game
{
    public Game(GameConfig gameConfig)
    {
        Context = this;

        AssetsContext = gameConfig.AssetsContext;
        LevelStructure = gameConfig.LevelStructure;
        AbilitiesConfig = gameConfig.AbilitiesConfig;
        AbilitiesConfig.Initialize();

        Player = ObjectUtils.CreateGameObject<Player>(gameConfig.PlayerPrefab);
        RootCanvas = ObjectUtils.CreateGameObject<Canvas>(gameConfig.RootCanvas.gameObject);
        PlayerUI = ObjectUtils.CreateGameObject<PlayerUI>(gameConfig.PlayerUIPrefab, RootCanvas.transform);

        PlayerUI.Initialize(Player);
    }

    public static Game Context { get; private set; }

    public AssetsContext AssetsContext { get; private set; }
    public AbilitiesConfig AbilitiesConfig { get; private set; }
    public LevelStructure LevelStructure { get; private set; }
    public Player Player { get; private set; }
    public Canvas RootCanvas { get; private set; }
    public PlayerUI PlayerUI { get; private set; }
}
