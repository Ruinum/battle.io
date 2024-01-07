using UnityEngine;

public class BuildDisabler : MonoBehaviour
{
    [SerializeField] private BuildInfoConfig _config;
    [SerializeField] private BuildType _buildType;

    private void Awake()
    {
        if (_config.BuildType != _buildType) gameObject.SetActive(false);
    }
}