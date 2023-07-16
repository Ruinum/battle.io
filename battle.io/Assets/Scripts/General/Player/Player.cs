using Ruinum.Core;
using System;
using UnityEngine;
using System.Collections.Generic;

public class Player : Executable, IPlayer
{
    [SerializeField] private AssetsContext _context;
    [SerializeField] private PlayerAnimatorController _animationController;
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private Movement _movement;
    
    [SerializeField] private float _magniteRadius;
    [SerializeField] private float _magniteSpeed;

    [SerializeField]private List<int> _takedWeapon = new List<int>();

    private Level _level;
    private ScaleView _scaleView;
    private Magnite _magnite;

    public Transform Transform => transform;
    public Level Level => _level;

    public override void Start()
    {        
        _level = GetComponent<Level>();
        _magnite = new Magnite(transform, _magniteSpeed, _magniteRadius);
        _scaleView = new ScaleView(transform);

        new HitBoxEvents(_animationController, _inventory);
        new WeaponAnimation(_animationController, _inventory);
        new AudioEvent(_animationController, _inventory);
        new Invulnerability(_level);

        AssetsInjector.Inject(_context, new HitImpact(_level, transform));

        Level.OnLevelChange += LevelUp;

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

    public void LevelUp(int i)
    {
        LevelStructure structure = LevelProgressionSystem.Singleton.levelStructure.GetLevel(_takedWeapon.ToArray(), 0);
        WeaponChooseUI.Singleton.GetChoose(structure);
    }

    public void TakeLevel(LevelStructure level,int i)
    {
        _takedWeapon.Add(i);
        if (level.Left) { _inventory.EquipWeapon(level.Left); } else { _inventory.Unarm(WeaponHandType.Left); }
        if (level.Right) { _inventory.EquipWeapon(level.Right); } else { _inventory.Unarm(WeaponHandType.Right); }
    }

    public IMovement GetMovement() => _movement;
    public ScaleView GetScaleView() => _scaleView;
}