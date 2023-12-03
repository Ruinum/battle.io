public class StarPool : Pool<Star>
{
    public StarPool(AssetsContext assetsContext, string poolName, string poolObjectName, int capacityPool) : base(assetsContext, poolName, poolObjectName, capacityPool)
    {
    }
}