using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class RewardAdButton : MonoBehaviour
{
    [SerializeField] private int _rewardID = 0;

    public void OpenRewardAd()
    {
        YandexGame.RewVideoShow(_rewardID);
    }
}