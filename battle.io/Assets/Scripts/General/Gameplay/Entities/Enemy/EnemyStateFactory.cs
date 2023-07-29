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
    public EnemyBaseState AggresiveIdleState() => new EnemyAggresiveIdle(_context, this);
    public EnemyBaseState HuntState() => new EnemyHunt(_context, this);
    public EnemyBaseState AwareState() => new EnemyAware(_context, this);
    public EnemyBaseState FleaState() => new EnemyFlea(_context, this);
}
