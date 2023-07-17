using Ruinum.Core;
using UnityEngine;

public class Player : Executable, IPlayer
{
    [SerializeField] private AssetsContext _context;
    [SerializeField] private PlayerAnimatorController _animationController;
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private Movement _movement;
    
    [SerializeField] private float _magniteRadius;
    [SerializeField] private float _magniteSpeed;

    private Level _level;
    private PlayerLevelProgression _levelProgression;
    private ScaleView _scaleView;
    private Magnite _magnite;

    public Transform Transform => transform;
    public Level Level => _level;
    public PlayerLevelProgression LevelProgression => _levelProgression;
    public ScaleView ScaleView => _scaleView;
    public IMovement Movement => _movement;

    public override void Start()
    {        
        _level = GetComponent<Level>();
        _magnite = new Magnite(transform, _magniteSpeed, _magniteRadius);
        _scaleView = new ScaleView(transform);
        _levelProgression = new PlayerLevelProgression(_level, _inventory);

        new HitBoxEvents(_animationController, _inventory);
        new WeaponAnimation(_animationController, _inventory);
        new AudioEvent(_animationController, _inventory);
        new SpecialEvent(_animationController, _inventory);
        new Invulnerability(_level);

        AssetsInjector.Inject(_context, new HitImpact(_level, transform));

        base.Start();
    }

    public override void Execute()
    {
        _magnite.Execute();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_inventory.TryGetRightWeapon(out var weaponInfo)) return;
            _animationController.PlayAnimation(weaponInfo.Type + " Attack");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!_inventory.TryGetLeftWeapon(out var weaponInfo)) return;
            _animationController.PlayAnimation(weaponInfo.Type + " Attack");
        }
    }
}
