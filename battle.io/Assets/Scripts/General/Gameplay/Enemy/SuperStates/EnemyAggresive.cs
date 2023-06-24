public sealed class EnemyAggresive : EnemyBaseState
{
    public EnemyAggresive(EnemyContext context, EnemyStateFactory factory) : base(context, factory)
    {
        InitializeSubState();
    }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateState() { }

    public override void CheckSwitchConditions() { }

    public override void InitializeSubState()
    {
        SetSubState(_factory.SearchState());
    }
}