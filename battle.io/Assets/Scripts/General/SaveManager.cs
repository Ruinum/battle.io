using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Ruinum.Codec;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private List<Achievement> _achievements;

    private RuinumCodec _codec;
    private const string _publicKey = "XJKFNCYR";
    private const string _fileName = "Save.data";

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _codec = new RuinumCodec("RRHFXKTG");
        Save();
        Load();
    }

    public void Save()
    {
        string destination = Application.dataPath + _fileName;
        
        FileStream file;
        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        StreamWriter stream = new StreamWriter(file);
        string statsInfo = $"{_stats.KilledBattlers}:{_stats.GamesWinned}:{_stats.GamesLosed}:{_stats.CollectedExp}:{_stats.TimeSpendInGame}";
        var achivementInfo = "-";
        for (int i = 0; i < _achievements.Count; i++)
        {
            achivementInfo += $"{i}.{_achievements[i].Unlocked}:";
        }

        stream.WriteLine(_codec.Encode(statsInfo+achivementInfo, _publicKey));
        stream.Close();
    }

    //TODO: Loading
    public void Load()
    {
        var destination = Application.dataPath + _fileName;
        
        FileStream file;
        if (File.Exists(destination)) file = File.OpenRead(destination);
        else return;

        StreamReader stream = new StreamReader(file);
        var text = stream.ReadToEnd();
        var decodecText = _codec.Decode(text, _publicKey);
        Debug.LogWarning(decodecText);
    }
}