using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropOffHouse : Interactable
{
    

    [SerializeField]
    ItemGrid itemGrid;

    DepotText depotText;

    Dictionary<string, int> requirements = new Dictionary<string, int>();

    public int reward = 5;

    List<string> removeKeys = new List<string>();
    List<string> foundKeys = new List<string>(); 
    bool active = false;
    bool achieved = false;

    [SerializeField]
    float refreshTimer = 5f;
    float time = 0f;

    private void Start()
    {
        itemGrid.gameObject.SetActive(active);
        depotText = GetComponent<DepotText>();
        loadNewRequirements();
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
                GoalReached();
            } else
            {
                achieved = false;
            }

        } else
        {
            time += Time.deltaTime;
            if(time > refreshTimer)
            {
                time = 0f;
                loadNewRequirements();
            }
        }
        
    }

    public void loadNewRequirements()
    {
        int amountDuctTape = UnityEngine.Random.Range(1, 5);
        int amountPlanks = UnityEngine.Random.Range(0, 3);

        SetRequirement("DuctTape", amountDuctTape);
        SetRequirement("Plank", amountPlanks);
        achieved = false;
    }

    private void GoalReached()
    {

        achieved = true;

        itemGrid.gameObject.SetActive(false);
        depotText.Amount1.text = "";
        depotText.Amount2.text = "";
        depotText.Amount3.text = "";
        depotText.itemIcon1.SetActive(false);
        depotText.itemIcon2.SetActive(false);
        depotText.itemIcon3.SetActive(false);
        Debug.Log("thnx for the items");


        CoinUIField.instance.AddCoins(reward);

    }

    protected override void Interact()
    {

        if (!achieved)
        {
            itemGrid.gameObject.SetActive(!active);
            active = !active;
        }

    }

    protected override void LeaveSpace()
    {
        itemGrid.gameObject.SetActive(false);

        active = false;

    }

    protected override void SetRequirement(string key, int amount)
    {
        requirements[key] = amount;
    }

    public override Dictionary<string, int> GetRequirements()
    {
        return requirements;
    }

    protected override void SetAllRequirements(Dictionary<string, int> other)
    {
        requirements = other;
    }

}
