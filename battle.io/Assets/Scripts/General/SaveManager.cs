using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Ruinum.Codec;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private List<Achievement> _achievements;

    private RuinumCodec _codec = new RuinumCodec("RRHFXKTG");
    private const string _publicKey = "XJKFNCYR";
    private const string _fileName = "Save.data";

    public static SaveManager Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;

        DontDestroyOnLoad(this);
        Load();
    }

    public void Save()
    {
        string destination = Application.dataPath + _fileName;
        
        FileStream file;
        
        if (File.Exists(destination)) file = File.Open(destination, FileMode.Truncate);
        else file = File.Create(destination);

        StreamWriter stream = new StreamWriter(file);
        string statsInfo = $"{_stats.KilledBattlers}:{_stats.GamesWinned}:{_stats.GamesLosed}:{_stats.CollectedExp}:{_stats.TimeSpendInGame}";
        var achivementInfo = "-";
        for (int i = 0; i < _achievements.Count; i++)
        {
            achivementInfo += $"{_achievements[i].Unlocked}:";
        }        

        stream.WriteLine(_codec.Encode(statsInfo+achivementInfo, _publicKey));
        stream.Close();
    }

    public void Load()
    {
        var destination = Application.dataPath + _fileName;
        
        FileStream file;
        if (File.Exists(destination)) file = File.OpenRead(destination);
        else return;

        StreamReader stream = new StreamReader(file);
        var text = stream.ReadToEnd();
        var decodecText = _codec.Decode(text, _publicKey);

        var textParts = decodecText.Split('-');
        var statsText = textParts[0].Split(':');
        var achievementText = textParts[1].Split(':');

        _stats.KilledBattlers = int.Parse(statsText[0]);
        _stats.GamesWinned = int.Parse(statsText[1]);
        _stats.GamesLosed = int.Parse(statsText[2]);
        _stats.CollectedExp = float.Parse(statsText[3]);
        _stats.TimeSpendInGame = float.Parse(statsText[4]);

        for (int i = 0; i < achievementText.Length - 1; i++)
        {
            _achievements[i].Unlocked = bool.Parse(achievementText[i]);
        }
    }
}