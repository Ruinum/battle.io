using UnityEngine;

public class FakePlayer : MonoBehaviour, IPlayer
{
    public Level Level => null;
    public Class Class => null;
    public ScaleView ScaleView => null;
    public IMovement Movement => null;
    public Transform Transform => transform;

    public bool IsDestroyed { get; set; }
}