using UnityEngine.AI;

public class NavMeshAgentRadius
{
    private NavMeshAgent _agent;
    private Level _level;
    private float _baseRadius;

    private const float NAVMESH_RADIUS_MODIFIER = 0.02f;

    public NavMeshAgentRadius(NavMeshAgent agent,Level level)
    {
        _agent = agent;
        _level = level;
        _level.OnExpChange += ChangeAgentRadius;
        _baseRadius = _agent.radius;
    }

    private void ChangeAgentRadius(float currentExp)
    {
        _agent.radius = _baseRadius + (NAVMESH_RADIUS_MODIFIER / _level.ExpNeeded * currentExp + _level.PlayerLevel * NAVMESH_RADIUS_MODIFIER);
    }
}
