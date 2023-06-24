using UnityEngine;

public sealed class EnemySearch : EnemyBaseState
{
    private Transform _transform;
    private EnemyMovement _movement;
    private EnemyRotateToPoint _rotate;

    private IInterestPoint _interestPoint;
    private float _currentDistance = 99f;
    private float _visionRadius;

    public EnemySearch(EnemyContext context, EnemyStateFactory factory) : base(context, factory)
    {
    }

    public override void EnterState()
    {
        _transform = _context.Transform;
        _movement = _context.Movement;
        _rotate = _context.Rotate;
        _visionRadius = _context.VisionRadius;

        _movement.OnDestination += FindInterestPoint;
        FindInterestPoint();
    }

    public override void ExitState()
    {
        _movement.OnDestination -= FindInterestPoint;
    }

    public override void UpdateState()
    {     
        if (_interestPoint == null) 
        {  
            FindInterestPoint();

            return;
        }

        _movement.Execute();
        _movement.SetPoint(_interestPoint.Transform.position);
        _rotate.SetPoint(_interestPoint.Transform.position);
        _rotate.Execute();
    }

    private void FindInterestPoint()
    {
        _currentDistance = 99f;
        var colliders = Physics2D.OverlapCircleAll(_transform.position, _visionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            var collider = colliders[i];
            if (collider.gameObject == _transform.gameObject) continue;
            if (!collider.TryGetComponent(out IInterestPoint interestPoint)) continue;

            float distance = Vector2.Distance(collider.transform.position, _transform.position);

            if (_currentDistance >= distance)
            {
                _interestPoint = interestPoint;
                _currentDistance = distance;
            }
        }
    }

    public override void InitializeSubState() { }
    public override void CheckSwitchConditions() { }
}