using Ruinum.Core;
using UnityEngine;

public class Enemy : Executable, IPlayer
{
    public Transform Transform => transform;
    public Level Level => _level;
    public EnemyVision Vision => _vision;
    public ScaleView ScaleView => _context.ScaleView;
    public Class Class => _context.Class;
    public IMovement Movement => _context.Movement;
    public ILevelProgression LevelProgression => _context.LevelProgression;

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

    private bool _isInitialized;

    public override void Start()
    {
        base.Start();

        Initialize();
    }

    public void Initialize()
    {
        if (_isInitialized) return;
        _level = GetComponent<Level>();
        _vision = new EnemyVision(transform, _level, VisionRadius);
        _magnite = new Magnite(transform, MagniteSpeed, MagniteRadius);

        _context = new EnemyContext(this);
        _states = new EnemyStateFactory(_context);

        _currentState = _states.IdleState();
        _currentState.EnterState();

        _isInitialized = true;
    }

    public override void Execute()
    {
        _currentState.UpdateStates();

        _vision.Execute();
        _magnite.Execute();       
    }

    public void FinalStage()
    {
        _context.FinalStage = true;

        _currentState = _states.IdleState();
        _currentState.EnterState();
    }

    public void Unload()
    {
        transform.transform.localScale = Vector3.one;
        Level.RemoveExp(999999999f);
    }

    public override void OnDestroy()
    {
        transform.transform.localScale = Vector3.one;

        base.OnDestroy();
        IsDestroyed = true;
    }
}