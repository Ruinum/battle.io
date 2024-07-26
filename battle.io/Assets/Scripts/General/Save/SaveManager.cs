using System.Collections.Generic;
using UnityEngine;
using Ruinum.Codec;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private BuildInfoConfig _buildInfo;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private SkinsConfig _skinsConfig;
    [SerializeField] private AchievementsConfig _achievementsConfig;

    private List<Achievement> _achievements = new List<Achievement>();
    private List<Skin> _skins = new List<Skin>();

    private ISave _save = null;
    private RuinumCodec _codec = new RuinumCodec("RRHFXKTG");
    private const string PUBLIC_KEY = "XJKFNCYR";

    public static SaveManager Singleton { get; private set; }

    private void Awake()
    {
        Singleton = this;

        DontDestroyOnLoad(this);
        Load();
    }

    private void Start()
    {
        _achievements.AddRange(_achievementsConfig.Achievements.ToArray());
        _skins.AddRange(_skinsConfig.Skins.ToArray());

        switch (_buildInfo.BuildType)
        {
            case BuildType.Desktop: _save = new DesktopSave(); break;
            case BuildType.Webgl: _save = new WebglSave(); break;
        }
    }

    public void Save()
    {
        if (_save == null) 
        {
            if (EditorConstants.Logging) Debug.LogError($"There is no saving implementation!, {typeof(SaveManager)}");
            return; 
        }

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

        _save.Save(_codec.Encode(statsInfo + achivementInfo + skinsInfo, PUBLIC_KEY));
    }

    public void Load()
    {
        if (_save == null)
        {
            if (EditorConstants.Logging) Debug.LogError($"There is no saving implementation, {typeof(SaveManager)}");
            return;
        }

        if (!_save.Load(out string text))
        {
            if (EditorConstants.Logging) Debug.LogError($"Loading error in save implementation, {typeof(SaveManager)}");
            return;
        }

        var decodecText = _codec.Decode(text, PUBLIC_KEY);

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

    public void SetSaveImplementation(ISave saveImplementation)
    {
        _save = saveImplementation;
    }
}