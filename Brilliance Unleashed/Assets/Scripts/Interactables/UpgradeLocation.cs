using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLocation : Interactable
{
    [SerializeField]
    UIShop uiShop;

    bool active = false;


    protected override void Interact()
    {

        if (active) uiShop.Show();
        else uiShop.Hide();

        active = !active;

    }

    protected override void LeaveSpace()
    {
        uiShop.Hide();
        active = false;
    }
}
