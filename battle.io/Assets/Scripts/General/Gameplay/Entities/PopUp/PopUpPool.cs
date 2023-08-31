public class PopUpPool : Pool<PopUp>
{
    public PopUpPool(AssetsContext assetsContext, string poolName, string poolObjectName, int capacityPool) : base(assetsContext, poolName, poolObjectName, capacityPool)
    {
    }
}
