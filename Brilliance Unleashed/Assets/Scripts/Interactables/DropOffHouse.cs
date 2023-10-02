using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffHouse : Interactable
{
    [SerializeField]
    UIShop uiShop;

    [SerializeField]
    ItemGrid itemGrid;

    [SerializeField]
    public Dictionary<string, int> requirements = new Dictionary<string, int>{ { "DuctTape", 3 }, { "Diamond", 1 } };

    public int reward = 5;

    List<string> removeKeys = new List<string>();
    List<string> foundKeys = new List<string>(); 
    bool active = false;

    bool achieved = false;

    private void Start()
    {
        itemGrid.gameObject.SetActive(active);
    }

    
    private void Update()
    {
        if (!achieved)
        {
            foundKeys.Clear();
            removeKeys.Clear();
            //Find if an item in grid matches a requirement.
            foreach (string key in requirements.Keys)
            {

                //Delete requirement if enough items were placed.
                if(requirements[key] <= 0)
                {
                    removeKeys.Add(key);
                    continue;
                }

                InventoryItem match = itemGrid.FindRequiredItem(key);
                if(match != null)
                {
                
                    foundKeys.Add(key);

                    itemGrid.CleanGridReference(match);
                    Destroy(match.gameObject);
                }      
            }

            foreach(string key in foundKeys)
            {
                //Decrease the requirement.
                requirements[key] -= 1;
            }

            foreach(string key in removeKeys)
            {
                requirements.Remove(key);
            }
        
            //If no requirements exist then the goal is reached
            if(requirements.Count == 0)
            {
                achieved = true;
                GoalReached();
            }
        }
        
    }

    private void GoalReached()
    {
        CoinUIField.instance.AddCoins(reward);
    }

    protected override void Interact()
    {
        itemGrid.gameObject.SetActive(!active);
        active = !active;
        if(active) uiShop.Show();
        else uiShop.Hide();
    }

    protected override void LeaveSpace()
    {
        itemGrid.gameObject.SetActive(false);
        active = !active;

        uiShop.Hide();
    }
}
