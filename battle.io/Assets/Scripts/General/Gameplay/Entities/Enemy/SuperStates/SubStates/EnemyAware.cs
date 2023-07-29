using UnityEngine;

public sealed class EnemyAware : EnemyBaseState
{
    private Transform _transform;
    private EnemyVision _vision;
    private float _fleaDistance;
    private IPlayer _nearestEnemy;

    public EnemyAware(EnemyContext context, EnemyStateFactory factory) : base(context, factory)
    {
        _transform = context.Transform;
    }

    public override void CheckSwitchConditions()
    {
        if (Vector2.Distance(_nearestEnemy.Transform.position, _transform.position) <= _fleaDistance) SwitchState(_factory.FleaState());
    }

    public override void EnterState()
    {
        _fleaDistance = _context.FleaDistance;
        _vision.OnNearestEnemyChange += ChangeEnemy;
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchConditions();
    }

    private void ChangeEnemy(IPlayer enemy) => _nearestEnemy = enemy;
}