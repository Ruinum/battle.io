using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;
    [SerializeField] private LevelStructure[] _levelStructures;
    [SerializeField] private float _maxTime = 0.25f;
    
    private PlayerSkinView _skinView;
    private ILevelProgression _levelProgression;
    private float _currentTime = 0;

    private void Start()
    {
        _skinView = GetComponent<PlayerSkinView>();
        _levelProgression = GetComponent<Player>().LevelProgression;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _maxTime)
        {
            _skinView.SetSkin(_skins[Random.Range(0, _skins.Length)]);
            _levelProgression.TakeLevel(_levelStructures[Random.Range(0, _levelStructures.Length)], 0);

            _currentTime = 0;
        }
    }  
}