using Ruinum.Utils;
using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Level _level;
    private ScaleView _scaleView;

    private IPlayer _player;
    private IMovement _movement;

    private float _currentLevelExp = 1;
    private float _scaleModifier = 0.2f;
    private float _speedModifier = 0.05f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        _level = this.GetComponentInObject<Level>();
        _player = GetComponent<IPlayer>();

        _movement = _player.Movement;
        _scaleView = _player.ScaleView;

        _level.OnExpChange += ChangeView;
    }

    private float CalculateModifier(float modifier) => (modifier / _level.ExpNeeded * _currentLevelExp + _level.PlayerLevel * modifier);

    private void ChangeView(float currentExpAmount)
    {
        _currentLevelExp = currentExpAmount;    

        _scaleView.ChangeScale(1 + CalculateModifier(_scaleModifier) - _scaleModifier);
        _movement.Modifier = Mathf.Max(0.8f, 1 - CalculateModifier(_speedModifier) + _speedModifier);
    }
}