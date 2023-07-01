using UnityEngine;

public class HitImpact
{
    private Transform _transform;
    [InjectAsset("ExpOrb")] private GameObject _expOrb;

    public HitImpact(Level level, Transform transform)
    {
        level.OnExpRemove += OnExpRemove;
        _transform = transform;
    }

    private void OnExpRemove(float removedExp)
    {
        int expOrbAmount = Random.Range(3, 5);
        float expOrbValue = removedExp / expOrbAmount;

        for (int i = 0; i < expOrbAmount; i++)
        {
            ExpOrb expOrb = Object.Instantiate(_expOrb, _transform.position, Quaternion.identity, null).GetComponent<ExpOrb>();
            expOrb.GetComponent<Collider2D>().enabled = false;
            expOrb.SetExp(expOrbValue);

            Vector2 direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            float randomSpeed = Random.Range(5f, 8f);

            SimulatePhysicUtils.Impulse(expOrb.gameObject, direction * 10, randomSpeed);
        }
    }
}
