using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    private void Update()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = worldPosition - new Vector2(transform.position.x, transform.position.y);
    }
}
