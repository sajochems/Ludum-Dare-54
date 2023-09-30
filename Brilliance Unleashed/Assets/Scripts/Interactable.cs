using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    public string promptMessage;

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

}
