public sealed class EnemyAggresive : EnemyBaseState
{
    private EnemyAttack _attack;
    private EnemyVision _vision;
    private EnemyRotateToPoint _rotate;

    public EnemyAggresive(EnemyContext context, EnemyStateFactory factory) : base(context, factory)
    {
        _isRootState = true;
        InitializeSubState();

        _vision = context.Vision;
        _attack = _context.Attack;
        _rotate = _context.Rotate;

        _vision.OnNearestEnemyChange += _attack.SetEnemy;
    }

    public override void EnterState() { }
    public override void ExitState()
    {
        _vision.OnNearestEnemyChange -= _attack.SetEnemy;
    }
    public override void UpdateState()
    {
        _attack.Execute();

        if (_vision.NearestEnemy != null && _vision.NearestEnemy != default)
        { 
        _rotate.SetPoint(_vision.NearestEnemy.Transform.position);
        _rotate.Execute();
        }

        CheckSwitchConditions();
    }

    public override void CheckSwitchConditions() 
    {
        if (_vision.Enemies.Count <= 0) SwitchState(_factory.IdleState());
    }

    public override void InitializeSubState()
    {
        SetSubState(_factory.AggresiveIdleState());
    }
}