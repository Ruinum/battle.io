public class EnemyIdle : EnemyBaseState
{
    public EnemyIdle(EnemyContext context, EnemyStateFactory factory) : base(context, factory)
    {
        _isRootState = true;
        InitializeSubState();
    }

    public override void InitializeSubState()
    {
        SetSubState(_factory.SearchState());
    }

    public override void UpdateState() { CheckSwitchConditions(); }
    public override void EnterState() { }
    public override void ExitState() { }

    public override void CheckSwitchConditions()
    {
        if (_context.Vision.Enemies.Count > 0) SwitchState(_factory.AggresiveState());
    }
}