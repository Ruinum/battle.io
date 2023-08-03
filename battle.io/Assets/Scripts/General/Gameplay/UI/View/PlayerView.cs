using Ruinum.Utils;
using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Level _level;
    private ScaleView _scaleView;
    private IMovement _movement;

    private const float SCALE_MODIFIER = 0.2f;
    private const float SPEED_MODIFIER = 0.05f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        _level = this.GetComponentInObject<Level>();
        var player = GetComponent<IPlayer>();

        _movement = player.Movement;
        _scaleView = player.ScaleView;

        _level.OnExpChange += ChangeView;
    }

    private float CalculateModifier(float modifier) => (modifier / _level.ExpNeeded * _level.Exp + _level.PlayerLevel * modifier);

    private void ChangeView(float currentExpAmount)
    {
        _scaleView.ChangeScale(1 + CalculateModifier(SCALE_MODIFIER) - SCALE_MODIFIER);
        _movement.Modifier = Mathf.Max(0.8f, 1 - CalculateModifier(SPEED_MODIFIER) + SPEED_MODIFIER);
    }
}