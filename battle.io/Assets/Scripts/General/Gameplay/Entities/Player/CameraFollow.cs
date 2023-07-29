using Ruinum.Core;
using UnityEngine;

public class CameraFollow : Executable
{
    [SerializeField] private float speed = 2.0f;

    private Transform _player;

    public void Initialize(Player player)
    {
        _player = player.transform;
    }

    public override void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        base.Start();
    }

    public override void Execute()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = transform.position;
        position.y = Mathf.Lerp(position.y, _player.transform.position.y, interpolation);
        position.x = Mathf.Lerp(position.x, _player.transform.position.x, interpolation);

        transform.position = position;
    }
}
