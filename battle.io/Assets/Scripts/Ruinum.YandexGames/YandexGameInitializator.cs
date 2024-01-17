using UnityEngine;

public class YandexGameInitializator : MonoBehaviour
{
    [SerializeField] private BuildInfoConfig _buildInfo;
    [SerializeField] private GameObject _yandexGamePrefab;

    private void Start()
    {
        if (_buildInfo.BuildType == BuildType.YandexGames)
        {
            Instantiate(_yandexGamePrefab);
        }

        Destroy(gameObject);
    }
}