using Ruinum.Core;

public class Enemy : Executable
{
    public float VisionRadius;
    public float MovementSpeed;

    public float MagniteSpeed;
    public float MagniteRadius;

    private EnemyContext _context;
    private EnemyBaseState _currentState;
    
    private Magnite _magnite;

    public override void Start()
    {
        base.Start();

        _magnite = new Magnite(transform, MagniteSpeed, MagniteRadius);

        _context = new EnemyContext(this);
        _context.SwitchState(_context.SearchState());
    }

    public override void Execute()
    {
        _currentState.UpdateState(_context);
        _currentState.CheckSwitchConditions(_context);

        _magnite.Execute();
    }

    public void SwitchState(EnemyBaseState state) { _currentState = state; _currentState.EnterState(_context); }
}
