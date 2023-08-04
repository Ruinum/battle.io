using UnityEngine;
using UnityEngine.AI;

public class EnemyContext
{
    public EnemyContext(Enemy enemy)
    {
        _enemy = enemy;

        Context = enemy.Context;
        Transform = enemy.transform;
        Level = enemy.Level;

        Rigidbody = enemy.GetComponent<Rigidbody2D>();
        Animator = enemy.GetComponent<PlayerAnimatorController>();
        Inventory = enemy.GetComponent<WeaponInventory>();
        Agent = enemy.GetComponent<NavMeshAgent>(); 

        Movement = new EnemyMovement(Agent, _enemy.MovementSpeed);
        Attack = new EnemyAttack(this);
        Rotate = new EnemyRotateToPoint(enemy.Model);
        ScaleView = new ScaleView(enemy.transform);
        LevelProgression = new EnemyLevelProgression(Level, Inventory);
        Class = new Class(_enemy.gameObject, Animator);

        new HitBoxEvents(Level, Animator, Inventory);
        new WeaponAnimation(Animator, Inventory);
        new AudioEvent(Animator, Inventory);
        new SpecialEvent(Animator, Inventory);
        new Invulnerability(Level);
        new NavMeshAgentRadius(Agent, Level);

        AssetsInjector.Inject(Context, new HitImpact(Level, Transform));
        
        Agent.speed = _enemy.MovementSpeed;
        Agent.updateRotation = false;
        Agent.acceleration = _enemy.Acceleration;

        Vector2 randomPoint = new Vector2(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f));
        Rotate.SetPoint(randomPoint);
        Agent.SetDestination(randomPoint);

        VisionRadius = _enemy.VisionRadius;
        DangerLevel = 2;
        FleaDistance = 2f;
        CalmDistance = 3f;
    }

    private Enemy _enemy;

    public EnemyVision Vision => _enemy.Vision;
    public AssetsContext Context { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerAnimatorController Animator { get; private set; }
    public WeaponInventory Inventory { get; private set; }
    public Level Level { get; private set; }
    public EnemyLevelProgression LevelProgression { get; private set; }
    public Class Class { get; private set; }
    public EnemyMovement Movement { get; private set; }
    public EnemyAttack Attack { get; private set; }
    public EnemyRotateToPoint Rotate { get; private set; }
    public ScaleView ScaleView { get; private set; }    
    public float VisionRadius { get; private set; }
    public int DangerLevel { get; private set; }
    public float FleaDistance { get; private set; }
    public float CalmDistance { get; private set; }

    public EnemyBaseState CurrentState { get { return _enemy.CurrentState; } set { _enemy.CurrentState = value; } }
}
