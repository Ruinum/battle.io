using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected abstract void Interact(Collider2D collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interact(collision);
    }
}
