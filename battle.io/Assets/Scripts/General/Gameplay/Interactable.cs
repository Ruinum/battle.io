using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Action OnInteract;

    private void Awake()
    {
        gameObject.layer = 9; //Layer Interactable
    }

    protected abstract void Interact(Collider2D collision);
    protected abstract void PlayerInteract(Collider2D collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") PlayerInteract(collision);
        else Interact(collision);
    }
}