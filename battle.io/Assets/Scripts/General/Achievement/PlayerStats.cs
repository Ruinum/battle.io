using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerStats), menuName = EditorConstants.DataPath + nameof(PlayerStats))]
public class PlayerStats : UniqueObject
{
    public int KilledBattlers;
    public int GamesWinned;
    public int GamesLosed;
    public float CollectedExp;
    public float TimeSpendInGame;

    public Action<int> OnKillsChanged;
    public Action<int> OnWinsChanged;
    public Action<int> OnLosesChanged;
    public Action<float> OnExpChanged;
    public Action<float> OnTimeSpendedChanged;
}