public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyContext context);

    public abstract void UpdateState(EnemyContext context);

    public abstract void CheckSwitchConditions(EnemyContext context);
}
