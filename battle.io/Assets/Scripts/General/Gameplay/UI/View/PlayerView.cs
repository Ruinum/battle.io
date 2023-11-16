using Ruinum.Utils;
using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Level _level;
    private ScaleView _scaleView;
    private IMovement _movement;

    [SerializeField] private float _scaleModifier = 0.2f;
    [SerializeField] private float _speedModifier = 0.05f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        _level = this.GetComponentInObject<Level>();
        var player = GetComponent<IPlayer>();

        _movement = player.Movement;
        _scaleView = player.ScaleView;

        _level.OnExpChange += ChangeView;
    }

    private float CalculateModifier(float modifier) => (modifier / _level.ExpNeeded * Mathf.Max(0, _level.Exp) + _level.PlayerLevel * modifier);

    private void ChangeView(float currentExpAmount)
    {
        _scaleView.ChangeScale(1 + CalculateModifier(_scaleModifier) - _scaleModifier);
        _movement.Modifier = Mathf.Max(0.8f, 1 - CalculateModifier(_speedModifier) + _speedModifier);
    }
}