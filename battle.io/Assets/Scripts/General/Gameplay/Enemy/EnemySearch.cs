using UnityEngine;

public class EnemySearch : EnemyBaseState
{
    private Transform _transform;
    private EnemyMovement _movement;

    private Transform _interestPoint;
    private float _currentDistance = 0f;
    private float _visionRadius;

    public override void CheckSwitchConditions(EnemyContext context)
    {
        
    }

    public override void EnterState(EnemyContext context)
    {
        _transform = context.Transform;
        _movement = context.Movement;
        _visionRadius = context.VisionRadius;
    }

    public override void UpdateState(EnemyContext context)
    {
        var colliders = Physics2D.OverlapCircleAll(_transform.position, _visionRadius);     

        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent<IInterestPoint>(out var interestPoint)) continue;

            float distance = Vector2.Distance(collider.transform.position, _transform.position);
            if (_currentDistance <= distance)
            {
                _interestPoint = collider.transform;
                _movement.SetPoint(_interestPoint.position);
            }
        }

        _movement.Move();
    }
}