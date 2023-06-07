using UnityEngine;

public class EnemySearch : EnemyBaseState
{
    private Transform _transform;
    private EnemyMovement _movement;

    private Transform _interestPoint;
    private float _currentDistance = 0f;
    private float _visionRadius;

    private float _currentVisionRadius;

    public override void CheckSwitchConditions(EnemyContext context)
    {
        
    }

    public override void EnterState(EnemyContext context)
    {
        _transform = context.Transform;
        _movement = context.Movement;
        _visionRadius = context.VisionRadius;

        _currentVisionRadius = _visionRadius;
    }

    public override void UpdateState(EnemyContext context)
    {
        if (_interestPoint == null) 
        { 
            _currentVisionRadius = _visionRadius * 3; 
            FindInterestPoint();

            return;
        }

        _movement.Move();
        _movement.SetPoint(_interestPoint.position);
    }

    private void FindInterestPoint()
    {
        var colliders = Physics2D.OverlapCircleAll(_transform.position, _currentVisionRadius);

        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out IInterestPoint interestPoint)) continue;

            float distance = Vector2.Distance(collider.transform.position, _transform.position);
            if (_currentDistance <= distance)
            {
                _interestPoint = collider.transform;
                _currentVisionRadius = _visionRadius;
            }
        }
    }
}