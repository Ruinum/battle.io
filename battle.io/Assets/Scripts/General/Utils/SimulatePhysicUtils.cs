using UnityEngine;

public static class SimulatePhysicUtils
{
    public static void Impulse(this GameObject gameObject, Vector3 position, float speed)
    {
        SimulatePhysicSystem.Singleton.ImpulseObject(gameObject, position, speed);
    }
}