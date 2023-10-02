using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLocation : Interactable
{
    [SerializeField]
    UIShop uiShop;

    [SerializeField] AudioSource audioSource;

    bool active = false;


    protected override void Interact()
    {

        if (active) uiShop.Show();
        else uiShop.Hide();

        active = !active;

        audioSource.PlayOneShot(audioSource.clip, 1);

    }

    protected override void LeaveSpace()
    {
        uiShop.Hide();
        active = false;
    }
}
