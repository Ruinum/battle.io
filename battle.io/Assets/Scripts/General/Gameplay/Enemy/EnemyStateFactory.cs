public class EnemyStateFactory
{
    private EnemyContext _context;

    public EnemyStateFactory(EnemyContext context)
    {
        _context = context;
    }

    public EnemyBaseState IdleState() => new EnemyIdle(_context, this);
    public EnemyBaseState SearchState() => new EnemySearch(_context, this);
    public EnemyBaseState AggresiveState() => new EnemyAggresive(_context, this);
    public EnemyBaseState EnemyAgressiveIdleState() => new EnemyAggresiveIdle(_context, this);
    public EnemyBaseState EnemyHuntState() => new EnemyHunt(_context, this);
    public EnemyBaseState EnemyAwareState() => new EnemyAwareState(_context, this);
}
