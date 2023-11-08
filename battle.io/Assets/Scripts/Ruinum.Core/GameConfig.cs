using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameConfig), menuName = EditorConstants.DataPath + nameof(GameConfig))]
public sealed class GameConfig : ScriptableObject
{
    public AssetsContext AssetsContext;
    public PlayerStats PlayerStats;
    public AbilitiesConfig AbilitiesConfig;
    public LevelStructure LevelStructure;
    public Canvas RootCanvas;

    [Header("Player Prefabs")]
    public GameObject PlayerPrefab;
    public GameObject PlayerUIPrefab;

    [Header("Exp Orb Pool Settings")]
    public int ExpOrbCapacity;
    public int ExpOrbHitImpactCapacity;

    [Header("PopUp Pool Settings")]
    public int PopUpCapacity;

    [Header("Achievements")]
    public List<Achievement> KillAchievement;
    public List<Achievement> WinAchievements;
    public List<Achievement> LoseAchievements;
    public List<Achievement> ExpAchievements;
    public List<Achievement> TimeAchievements;
}