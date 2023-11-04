using Ruinum.Core;
using Ruinum.Core.Systems;
using Ruinum.Utils;
using System;
using UnityEngine;

public class Player : Executable, IPlayer
{
    [SerializeField] private AssetsContext _context;
    [SerializeField] private PlayerWeaponAnimatorController _animationController;
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private Movement _movement;
    [SerializeField] private GameObject _cameraPrefab;

    [SerializeField] private float _maxLevel;
    [SerializeField] private float _magniteRadius;
    [SerializeField] private float _magniteSpeed;
    [SerializeField] private float _scaleCap;
    [SerializeField] private float _maxCameraSize;

    private Level _level;
    private Class _class;
    private PlayerLevelProgression _levelProgression;
    private ScaleView _scaleView;
    private CameraView _cameraView;
    private Magnite _magnite;
    private CameraFollow _cameraFollow;
    private bool _mainWeaponDelay;
    private bool _subWeaponDelay;

    public Transform Transform => transform;
    public Level Level => _level;
    public Class Class => _class;
    public PlayerLevelProgression LevelProgression => _levelProgression;
    public ScaleView ScaleView => _scaleView;
    public IMovement Movement => _movement;

    public bool IsDestroyed { get; set; }

    public void Awake()
    {   
        _level = GetComponent<Level>();
        _class = new Class(transform.gameObject, _animationController);
        _magnite = new Magnite(transform, _magniteSpeed, _magniteRadius);
        _scaleView = new ScaleView(transform, _scaleCap);
        _levelProgression = new PlayerLevelProgression(this, _level, _inventory);

        _cameraFollow = ObjectUtils.CreateGameObject<CameraFollow>(_cameraPrefab);
        _cameraView = new CameraView(_cameraFollow.Camera, _level, _maxCameraSize);
        
        _cameraFollow.Initialize(this);
    }

    public override void Start()
    {
        new WeaponAnimation(_animationController, _inventory);
        new AudioEvents(_animationController, _inventory);
        new SpecialEvent(_animationController, _inventory);
        new Invulnerability(_level);
        new HitBoxEvents(_level, _animationController, _inventory);

        AssetsInjector.Inject(_context, new HitImpact(_level, transform));
        _level.OnLevelChange += CheckFinalStage;

        base.Start();
    }

    private void CheckFinalStage(int currentLevel)
    {
        if (currentLevel == _maxLevel) Game.Context.FinalGame();
    }

    public override void Execute()
    {
        _magnite.Execute();
        _cameraView.Execute();
        _levelProgression.Execute();
        _class.Execute();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !_mainWeaponDelay)
        {
            if (!_inventory.TryGetRightWeapon(out var weaponInfo)) return;
            _animationController.PlayWeaponAttackAnimation(weaponInfo);
            TimerSystem.Singleton.StartTimer(weaponInfo.Animation.Clip.length, () => _mainWeaponDelay = false);
            _mainWeaponDelay = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !_subWeaponDelay)
        {
            if (!_inventory.TryGetLeftWeapon(out var weaponInfo)) return;
            _animationController.PlayWeaponAttackAnimation(weaponInfo);
            TimerSystem.Singleton.StartTimer(weaponInfo.Animation.Clip.length, () => _subWeaponDelay = false);
            _subWeaponDelay = true;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        IsDestroyed = true;
    }
}
