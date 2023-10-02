using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    PlayerMovement playerMovement;

    [SerializeField]
    ItemGrid inventoryGrid;

    [SerializeField]
    GameObject scoreTracker;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IncreaseSpeed();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            IncreaseInventorySize();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Victory();
        }
    }

    public bool Victory()
    {
        Debug.Log("Victory achieved");
        scoreTracker.GetComponent<Score>().running = false;
        return true;
    }

    public bool IncreaseInventorySize()
    {
        int newheight = inventoryGrid.gridSizeHeight;
        if (newheight >= 18)
        {
            return false;
        }

        newheight += 2;
        inventoryGrid.Resize(inventoryGrid.gridSizeWidth, newheight);
        return true;
    }

    public bool IncreaseSpeed()
    {
        if(playerMovement.moveSpeed >= 10f)
        {
            return false;
        }

        playerMovement.moveSpeed += 0.5f;
        return true;
    }
}
