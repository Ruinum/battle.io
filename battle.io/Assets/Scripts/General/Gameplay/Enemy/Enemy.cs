using Ruinum.Core;
using UnityEngine;
using System.Collections.Generic;

public class Enemy : Executable, IPlayer
{
    public Transform Transform => transform;
    public Level Level => _level;
    public EnemyVision Vision => _vision;

    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public AssetsContext Context;

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

        Level.OnLevelChange += Progress;
    }

    public override void Execute()
    {
        _currentState.UpdateStates();

        _vision.Execute();
        _magnite.Execute();

        if (Input.GetKeyDown(KeyCode.P)) Level.AddExp(100);
    }

    private List<int> _WeaponHave = new List<int>();
    public void Progress(int lvl)
    {
        LevelStructure level = LevelProgressionSystem.Singleton.levelStructure.GetLevel(_WeaponHave.ToArray(),0);
        if (level.nextLevel.Length == 0) return;
        int rnd = Random.Range(0, level.nextLevel.Length);
        level = level.nextLevel[rnd];
        _WeaponHave.Add(rnd);
        if (level.Left) { _context.Inventory.EquipWeapon(level.Left); } else { _context.Inventory.Unarm(WeaponHandType.Left); }
        if (level.Right) { _context.Inventory.EquipWeapon(level.Right); } else { _context.Inventory.Unarm(WeaponHandType.Right); }
    }

    public IMovement GetMovement() => _context.Movement;
    public ScaleView GetScaleView() => _context.ScaleView;
}
