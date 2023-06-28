using Ruinum.Core;
using UnityEngine;

public class Player : Executable, IPlayer
{
    [SerializeField] private PlayerAnimatorController _animationController;
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private Movement _movement;
    [SerializeField] private AnimationDatasConfig _animationsConfig;

    [SerializeField] private float _magniteRadius;
    [SerializeField] private float _magniteSpeed;

    private HitBoxController _hitBoxController;
    private Level _level;
    private ScaleView _scaleView;
    private Magnite _magnite;

    public Transform Transform => transform;
    public Level Level => _level;

    public override void Start()
    {
        _animationController.InitializeAnimations(_animationsConfig);

        _level = GetComponent<Level>();
        _magnite = new Magnite(transform, _magniteSpeed, _magniteRadius);
        _scaleView = new ScaleView(transform);
        _hitBoxController = new HitBoxController(_animationController, _inventory, _animationsConfig.AnimationDatas);

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

    public IMovement GetMovement() => _movement;
    public ScaleView GetScaleView() => _scaleView;
}