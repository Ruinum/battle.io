using UnityEngine;

public sealed class EnemyHunt : EnemyBaseState
{
    private EnemyVision _vision;
    private EnemyMovement _movement;
    private Level _level;
    
    private IPlayer _nearestEnemy;

    private int _dangerLevel;
    private float _minRandom = -3f;
    private float _maxRandom = 3f;

    public EnemyHunt(EnemyContext context, EnemyStateFactory factory) : base(context, factory)
    {
        _vision = _context.Vision;
        _movement = _context.Movement;
        _level = _context.Level;

        _movement.OnDestination += FindPoint;
    }

    public override void InitializeSubState()
    {
    }

    public override void EnterState()
    {
        _nearestEnemy = _vision.NearestEnemy;
        _dangerLevel = _context.DangerLevel;

        FindPoint();
    }

    public override void ExitState()
    {
        _movement.OnDestination -= FindPoint;
    }

    public override void UpdateState()
    {
        _movement.Execute();

        _nearestEnemy = _vision.NearestEnemy;
    }

    private void FindPoint()
    {
        if (_nearestEnemy.IsDestroyed) return;
        Vector3 point = new Vector3(_nearestEnemy.Transform.position.x + Random.Range(_minRandom, _maxRandom), _nearestEnemy.Transform.position.y + Random.Range(_minRandom, _maxRandom), 0f);
        _movement.SetPoint(point);
    }

    public override void CheckSwitchConditions()
    {
        if (_nearestEnemy.Level.PlayerLevel - _dangerLevel > _level.PlayerLevel && !Game.Context.FinalStage) SwitchState(_factory.AwareState()); 
    }
}