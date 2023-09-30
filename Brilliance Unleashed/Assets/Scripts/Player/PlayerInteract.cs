using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private PlayerText playerText;
    private PlayerInput playerInput;

    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        playerText = GetComponent<PlayerText>();
    }

    // Update is called once per frame
    void Update()
    {
        playerText.UpdateText(string.Empty);

        //Check if player is close enough
        //If this is the case do
        if(interactable != null)
        {
            playerText.UpdateText(interactable.promptMessage);
            if (playerInput.Player.Interact.triggered)
            {
                interactable.BaseInteract();
            }
        }   
    }

    public void SetInRange(Interactable other)
    {
        this.interactable = other;
    }
}
