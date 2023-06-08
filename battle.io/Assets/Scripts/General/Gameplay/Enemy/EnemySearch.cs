using UnityEngine;

public class EnemySearch : EnemyBaseState
{
    private Transform _transform;
    private EnemyMovement _movement;
    private EnemyRotateToPoint _rotate;

    private Transform _interestPoint;
    private float _currentDistance = 99f;
    private float _visionRadius;

    private float _currentVisionRadius;

    public override void CheckSwitchConditions(EnemyContext context)
    {
        
    }

    public override void EnterState(EnemyContext context)
    {
        _transform = context.Transform;
        _movement = context.Movement;
        _rotate = context.Rotate;
        _visionRadius = context.VisionRadius;

        _currentVisionRadius = _visionRadius;

        _movement.OnDestination += FindInterestPoint;
        FindInterestPoint();
    }

    public override void UpdateState(EnemyContext context)
    {     
        if (_interestPoint == null) 
        {  
            FindInterestPoint();

            return;
        }

        _movement.Move();
        _movement.SetPoint(_interestPoint.position);
        _rotate.SetPoint(_interestPoint.position);
        _rotate.Execute();
    }

    private void FindInterestPoint()
    {
        _currentDistance = 99f;
        var colliders = Physics2D.OverlapCircleAll(_transform.position, _currentVisionRadius);
        Debug.Log(colliders.Length);

        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out IInterestPoint interestPoint)) continue;

            float distance = Vector2.Distance(collider.transform.position, _transform.position);
            Debug.LogWarning($"{_currentDistance}, {distance}");

            if (_currentDistance >= distance)
            {
                _interestPoint = collider.transform;
                _currentVisionRadius = _visionRadius;
                _currentDistance = distance;
            }
        }
    }
}