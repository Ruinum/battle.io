public interface IPlayer : IInterestPoint
{
    Level Level { get; }
    Class Class { get; }
    ScaleView ScaleView { get; }
    IMovement Movement { get; }
    ILevelProgression LevelProgression { get; }
}
