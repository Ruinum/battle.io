using Ruinum.Core;
using UnityEngine;

public class Enemy : Executable, IPlayer
{
    public Transform Transform => transform;
    public Level Level => _level;
    public EnemyVision Vision => _vision;
    public ScaleView ScaleView => _context.ScaleView;
    public IMovement Movement => _context.Movement;

    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsDestroyed { get; set; }

    public AssetsContext Context;
    public Transform Model;

    public float VisionRadius;
    public float MovementSpeed;
    public float Acceleration;

    public float MagniteSpeed;
    public float MagniteRadius;

    private Level _level;
    private EnemyVision _vision;
    private Magnite _magnite;

    private EnemyStateFactory _states;
    private EnemyContext _context;
    private EnemyBaseState _currentState;

    public override void Start()
    {
        base.Start();

        _level = GetComponent<Level>();
        _vision = new EnemyVision(transform, _level, VisionRadius);
        _magnite = new Magnite(transform, MagniteSpeed, MagniteRadius);

        _context = new EnemyContext(this);
        _states = new EnemyStateFactory(_context);

        _currentState = _states.IdleState();
        _currentState.EnterState();
    }

    public override void Execute()
    {
        _currentState.UpdateStates();

        _vision.Execute();
        _magnite.Execute();

        if (Input.GetKeyDown(KeyCode.P)) Level.AddExp(100);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        IsDestroyed = true;
    }
}
