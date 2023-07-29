using UnityEngine.AI;

public class NavMeshAgentRadius
{
    private NavMeshAgent _agent;
    private Level _level;

    private float _baseRadius;
    private float _modifier = 0.1f;

    public NavMeshAgentRadius(NavMeshAgent agent,Level level)
    {
        _agent = agent;
        _level = level;
        _level.OnExpChange += ChangeAgentRadius;
        _baseRadius = _agent.radius;
    }

    private void ChangeAgentRadius(float currentExp)
    {
        _agent.radius = _baseRadius + (_modifier / _level.ExpNeeded * currentExp + _level.PlayerLevel * _modifier);
    }
}
