using UnityEngine;

public static class SimulatePhysicUtils
{
    public static void Impulse(this GameObject gameObject, Vector3 direction, float speed, float lifeTime = 0.5f, float mass = 0)
    {
        SimulatePhysicSystem.Singleton.ImpulseObject(gameObject, direction, speed, lifeTime, mass);
    }
}