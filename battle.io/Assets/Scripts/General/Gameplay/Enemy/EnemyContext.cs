using UnityEngine;

public class EnemyContext
{
    public EnemyContext(Enemy enemy)
    {
        _enemy = enemy;

        Transform = enemy.transform;
        Rigidbody = enemy.GetComponent<Rigidbody2D>();
        Movement = new EnemyMovement(Rigidbody, _enemy.MovementSpeed);
        
        VisionRadius = _enemy.VisionRadius;
    }

    private Enemy _enemy;

    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public EnemyMovement Movement { get; private set; }
    public float VisionRadius { get; private set; }

    public EnemyBaseState SearchState() => new EnemySearch();

    public void SwitchState(EnemyBaseState state) => _enemy.SwitchState(state);
}
