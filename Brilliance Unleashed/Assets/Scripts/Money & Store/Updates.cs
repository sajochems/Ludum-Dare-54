using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updates
{
    public enum UpdateType
    {
        MoreSpeed,
        MoreInventory,
        AchieveWealth
    }

    public static int GetCost(UpdateType updateType)
    {
        //TODO: Not hardcode the prices
        switch (updateType)
        {
            default: return 0;
            case UpdateType.MoreSpeed: return 10;
            case UpdateType.MoreInventory: return 50;
            case UpdateType.AchieveWealth: return 500;
        }
    }
}
