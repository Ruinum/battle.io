public sealed class ExpOrbPool : Pool<ExpOrb>
{
    public ExpOrbPool(AssetsContext assetsContext, string poolName, string poolObjectName, int capacityPool) : base(assetsContext, poolName, poolObjectName, capacityPool)
    {
    }
}