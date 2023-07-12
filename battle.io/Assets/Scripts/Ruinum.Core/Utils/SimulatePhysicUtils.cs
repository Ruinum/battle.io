using UnityEngine;

public static class SimulatePhysicUtils
{
    public static void Impulse(this GameObject gameObject, Vector3 direction, float speed)
    {
        SimulatePhysicSystem.Singleton.ImpulseObject(gameObject, direction, speed);
    }
}