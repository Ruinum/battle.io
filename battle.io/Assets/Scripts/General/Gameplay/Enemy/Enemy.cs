using Ruinum.Core;
using UnityEngine;

public class Enemy : Executable, IPlayer
{
    [SerializeField] private AnimationDatasConfig _animationsConfig;

    public Transform Transform => transform;
    public Level Level => _level;
    public EnemyVision Vision => _vision;
    public AnimationDatasConfig AnimationsConfig => _animationsConfig;

    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public float VisionRadius;
    public float MovementSpeed;

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
        _vision = new EnemyVision(transform, VisionRadius);
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
    }

    public IMovement GetMovement() => _context.Movement;
    public ScaleView GetScaleView() => _context.ScaleView;
}
