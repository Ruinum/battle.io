public interface IPlayer : IInterestPoint
{
    Level Level { get; }

    IMovement GetMovement();
    ScaleView GetScaleView();
}
