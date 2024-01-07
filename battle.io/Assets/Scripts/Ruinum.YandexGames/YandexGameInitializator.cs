using UnityEngine;

public class YandexGameInitializator : MonoBehaviour
{
    [SerializeField] private BuildInfoConfig _buildInfo;
    [SerializeField] private GameObject _yandexGamePrefab;

    private void Awake()
    {
        if (_buildInfo.BuildType == BuildType.YandexGames)
        {
            Instantiate(_yandexGamePrefab);
        }

        Destroy(gameObject);
    }
}