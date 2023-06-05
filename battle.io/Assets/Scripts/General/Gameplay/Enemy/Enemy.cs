using Ruinum.Core;
using UnityEngine;

public class Enemy : Executable
{
    public float VisionRadius;
    public float MovementSpeed;

    private EnemyContext _context;
    private EnemyBaseState _currentState;

    public override void Start()
    {
        base.Start();

        _context = new EnemyContext(this);
        _context.SwitchState(_context.SearchState());
    }

    public override void Execute()
    {
        _currentState.UpdateState(_context);
        _currentState.CheckSwitchConditions(_context);        
    }

    public void SwitchState(EnemyBaseState state) { _currentState = state; _currentState.EnterState(_context); }
}
