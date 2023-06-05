using Ruinum.Core;
using UnityEngine;

public class CameraFollow : Executable
{
    [SerializeField] private Transform _player;
    [SerializeField] private float speed = 2.0f;

    public override void Execute()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = transform.position;
        position.y = Mathf.Lerp(position.y, _player.transform.position.y, interpolation);
        position.x = Mathf.Lerp(position.x, _player.transform.position.x, interpolation);

        transform.position = position;
    }
}
