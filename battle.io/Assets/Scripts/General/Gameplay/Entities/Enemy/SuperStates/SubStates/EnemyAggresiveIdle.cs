public sealed class EnemyAggresiveIdle : EnemyBaseState
{
    private Level _level;
    private EnemyVision _vision;
    private int _dangerLevel;
    
    public EnemyAggresiveIdle(EnemyContext context, EnemyStateFactory factory) : base(context, factory)
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void EnterState()
    {
        _vision = _context.Vision;
        _level = _context.Level;

        _dangerLevel = _context.DangerLevel;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchConditions();
    }

    public override void CheckSwitchConditions()
    {
        if (_vision.NearestEnemy.Level.PlayerLevel - _dangerLevel > _level.PlayerLevel) SwitchState(_factory.AwareState());
        if (_vision.NearestEnemy.Level.PlayerLevel - _dangerLevel <= _level.PlayerLevel) SwitchState(_factory.HuntState());
    }
}