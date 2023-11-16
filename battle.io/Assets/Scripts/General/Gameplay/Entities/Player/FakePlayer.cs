using UnityEngine;

public class FakePlayer : MonoBehaviour, IPlayer
{
    public Transform Transform => transform;
    public Level Level => null;
    public Class Class => null;
    public ScaleView ScaleView => null;
    public IMovement Movement => null;
    public ILevelProgression LevelProgression => null;
    public bool IsDestroyed { get; set; }
}