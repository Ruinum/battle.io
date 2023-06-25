public abstract class EnemyBaseState
{
    protected EnemyContext _context;
    protected EnemyStateFactory _factory;
    protected EnemyBaseState _superState;
    protected EnemyBaseState _subState;
    protected bool _isRootState = false;

    public EnemyBaseState(EnemyContext context, EnemyStateFactory factory) 
    { 
        _context = context;
        _factory = factory;

        UnityEngine.Debug.Log(this);
    } 

    public abstract void EnterState();
    
    public abstract void ExitState();

    public abstract void UpdateState(); 
    
    public void UpdateStates()
    {
        UpdateState();

        if (_subState == null) return;
        _subState.UpdateStates();
    }

    public void ExitStates()
    {
        ExitState();

        if (_subState == null) return;
        _subState.ExitStates();
    }

    public abstract void InitializeSubState();

    public abstract void CheckSwitchConditions();

    protected void SwitchState(EnemyBaseState state)
    {
        ExitState();

        state.EnterState();

        if (_isRootState)
        {
            ExitStates();
            _context.CurrentState = state;
        } 
        else if (_subState != null)
        {
            _subState.ExitStates();
            _subState.SetSubState(state);
        }
    }

    protected void SetSuperState(EnemyBaseState superState)
    {
        _superState = superState;
    }

    protected void SetSubState(EnemyBaseState subState)
    {
        _subState = subState;
        _subState.SetSuperState(this);
        _subState.EnterState();
    }
}
