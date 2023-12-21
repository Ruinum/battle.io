using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Ruinum.Codec;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private SkinsConfig _skinsConfig;
    [SerializeField] private AchievementsConfig _achievementsConfig;

    private List<Achievement> _achievements;
    private List<Skin> _skins;

    private RuinumCodec _codec = new RuinumCodec("RRHFXKTG");
    private const string _publicKey = "XJKFNCYR";
    private const string _fileName = "Save.data";

    public static SaveManager Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;

        DontDestroyOnLoad(this);
        LoadFromFile();
    }

    private void Start()
    {
        _achievements.AddRange(_achievementsConfig.Achievements.ToArray());
        _skins.AddRange(_skinsConfig.Skins.ToArray());
    }

    public void SaveInFile()
    {
        string destination = Application.dataPath + _fileName;

        FileStream file;

        if (File.Exists(destination)) file = File.Open(destination, FileMode.Truncate);
        else file = File.Create(destination);

        StreamWriter stream = new StreamWriter(file);

        string saveData = Save();

        stream.WriteLine(saveData);
        stream.Close();
    }

    public string Save()
    {
        string statsInfo = $"{_stats.KilledBattlers}:{_stats.GamesWinned}:{_stats.GamesLosed}:{_stats.CollectedExp}:{_stats.TimeSpendInGame}:{_stats.Stars}";
        
        var achivementInfo = "-";
        for (int i = 0; i < _achievements.Count; i++)
        {
            achivementInfo += $"{_achievements[i].Unlocked}:";
        }
        
        var skinsInfo = "-";
        for (int i = 0; i < _skins.Count; i++)
        {
            skinsInfo += $"{_skins[i].Unlocked}:";
        }

        return _codec.Encode(statsInfo + achivementInfo + skinsInfo, _publicKey);
    }

    public void LoadFromFile()
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
        var skinsText = textParts[2].Split(':');

        _stats.KilledBattlers = int.Parse(statsText[0]);
        _stats.GamesWinned = int.Parse(statsText[1]);
        _stats.GamesLosed = int.Parse(statsText[2]);
        _stats.CollectedExp = float.Parse(statsText[3]);
        _stats.TimeSpendInGame = float.Parse(statsText[4]);
        _stats.Stars = int.Parse(statsText[5]);

        for (int i = 0; i < achievementText.Length - 1; i++)
        {
            _achievements[i].Unlocked = bool.Parse(achievementText[i]);
        }

        for (int i = 0; i < skinsText.Length - 1; i++)
        {
            _skins[i].Unlocked = bool.Parse(skinsText[i]);
        }
    }

    public string Decode(string encodedInfo)
    {
        return _codec.Decode(encodedInfo, _publicKey);
    }
}