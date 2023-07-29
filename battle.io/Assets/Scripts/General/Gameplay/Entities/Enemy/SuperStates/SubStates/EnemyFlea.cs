using UnityEngine;

public sealed class EnemyFlea : EnemyBaseState
{
    private Transform _transform;
    private EnemyVision _vision;
    private EnemyMovement _movement;
    private EnemyRotateToPoint _rotate;
    
    private Vector2 _direction;
    private float _calmDistance;

    public EnemyFlea(EnemyContext context, EnemyStateFactory factory) : base(context, factory)
    {
        _transform = context.Transform;
        _vision = context.Vision;
        _movement = context.Movement;
        _rotate = context.Rotate;

        _calmDistance = context.CalmDistance;
    }

    public override void EnterState()
    {        
        _movement.OnDestination += FindPoint;

        FindPoint();
    }

    public override void UpdateState()
    {
        _rotate.SetPoint(_vision.NearestEnemy.Transform.position);
        _rotate.Execute();

        CheckSwitchConditions();
    }

    public override void ExitState()
    {
        _movement.OnDestination -= FindPoint;
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchConditions()
    {
        if (_vision.NearestEnemy.IsDestroyed) SwitchState(_factory.AggresiveIdleState());
        if (Vector2.Distance(_vision.NearestEnemy.Transform.position, _transform.position) >= _calmDistance) SwitchState(_factory.AggresiveIdleState());
    }

    private void FindPoint()
    {
        if (_vision.NearestEnemy.IsDestroyed) return;
        _direction = (_vision.NearestEnemy.Transform.position - _transform.position).normalized;
        _movement.SetPoint(_direction * 3);
    }
}