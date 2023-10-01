using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    [SerializeField]
    public string promptMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInteract>().SetInRange(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInteract>().SetInRange(null);
            LeaveSpace();
        }
    }

    public virtual string OnPosition()
    {
        return promptMessage;
    }

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {

    }

    protected virtual void LeaveSpace()
    {

    }

}
