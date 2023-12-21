using UnityEngine;

[CreateAssetMenu(fileName = nameof(AchievementsConfig), menuName = EditorConstants.DataPath + nameof(AchievementsConfig))]
public class AchievementsConfig : UniqueObject
{
    public Achievement[] Achievements;
}
