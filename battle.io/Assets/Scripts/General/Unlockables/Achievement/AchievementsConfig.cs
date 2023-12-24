using UnityEngine;

[CreateAssetMenu(fileName = nameof(AchievementsConfig), menuName = EditorConstants.ConfigPath + nameof(AchievementsConfig))]
public class AchievementsConfig : UniqueObject
{
    public Achievement[] Achievements;
}
