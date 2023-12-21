using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameConfig), menuName = EditorConstants.DataPath + nameof(GameConfig))]
public sealed class GameConfig : ScriptableObject
{
    public LevelStructure LevelStructure;
    public Canvas RootCanvas;
    public Skin BasePlayerSkin;

    [Header("Configs")]
    public AssetsContext AssetsContext;
    public PlayerStats PlayerStats;
    public AchievementsConfig Achievements;
    public SkinsConfig Skins;
    public AbilitiesConfig AbilitiesConfig;

    [Header("Prefabs")]
    public GameObject PlayerPrefab;
    public GameObject PlayerUIPrefab;
    public GameObject AchievementPopUpUI;

    [Header("Exp Orb Pool Settings")]
    public int ExpOrbCapacity;
    public int ExpOrbHitImpactCapacity;

    [Header("PopUp Pool Settings")]
    public int PopUpCapacity;

    [Header("Star Pool Settings")]
    public int StarCapacity;

    [Header("Achievements")]
    public List<Achievement> KillAchievement;
    public List<Achievement> WinAchievements;
    public List<Achievement> LoseAchievements;
    public List<Achievement> ExpAchievements;
    public List<Achievement> TimeAchievements;
}