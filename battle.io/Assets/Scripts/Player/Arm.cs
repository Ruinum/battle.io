using UnityEngine;

public class Arm : MonoBehaviour
{
    private Transform _child;
    private bool _isOneHand;

    private void Update()
    {
        if (!_child || !_isOneHand) return;
        _child.position = transform.position;
    }

    public void SetChild(Transform child, bool isOneHand = true)
    {
        _child = child;
        _isOneHand = isOneHand;
    }
}
