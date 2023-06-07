public interface IMovement
{
    float Speed { get; }
    float Modifier { get; set; }
    
    void Move();
}
