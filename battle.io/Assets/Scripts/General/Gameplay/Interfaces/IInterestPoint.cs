using UnityEngine;

public interface IInterestPoint
{
    Transform Transform { get; }
    bool IsDestroyed { get; set; }
}
