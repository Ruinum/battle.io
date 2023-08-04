using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameConfig), menuName = EditorConstants.DataPath + nameof(GameConfig))]
public sealed class GameConfig : ScriptableObject
{
    public AssetsContext AssetsContext;
    public AbilitiesConfig AbilitiesConfig;
    public LevelStructure LevelStructure;
    public Canvas RootCanvas;

    [Header("Player Prefabs")]
    public GameObject PlayerPrefab;
    public GameObject PlayerUIPrefab;
}