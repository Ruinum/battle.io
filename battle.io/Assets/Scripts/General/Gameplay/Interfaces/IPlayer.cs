public interface IPlayer : IInterestPoint
{
    Level Level { get; }    
    ScaleView ScaleView { get; }
    IMovement Movement { get; }
}
