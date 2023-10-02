using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepotText : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI Amount1;

    [SerializeField]
    private TextMeshProUGUI Amount2;

    [SerializeField]
    private TextMeshProUGUI Amount3;

    [SerializeField]
    private GameObject itemIcon1;

    [SerializeField]
    private GameObject itemIcon2;

    [SerializeField]
    private GameObject itemIcon3;

    [SerializeField]
    List<ItemData> items;

    private Dictionary<string, int> requirements;

    // Start is called before the first frame update
    void Start()
    {
        requirements = GetComponent<Interactable>().GetRequirements();

        itemIcon1.SetActive(false);
        itemIcon2.SetActive(false);
        itemIcon3.SetActive(false);

        Amount1.text = "";
        Amount2.text = "";
        Amount3.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        int counter = 0;
        foreach(string key in requirements.Keys)
        {
            ItemData found = FindItem(items, key);
            if(counter == 0)
            {
                Amount1.text = requirements[key] + "X";
                itemIcon1.SetActive(true);
                itemIcon1.GetComponent<Image>().sprite = found.itemIcon;
            }
            if (counter == 1)
            {
                Amount2.text = requirements[key] + "X";
                itemIcon2.SetActive(true);
                itemIcon2.GetComponent<Image>().sprite = found.itemIcon;
            }
            if (counter == 2)
            {
                Amount3.text = requirements[key] + "X";
                itemIcon3.SetActive(true);
                itemIcon3.GetComponent<Image>().sprite = found.itemIcon;
            }
            counter += 1;
        }
    }

    public ItemData FindItem(List<ItemData> list, string key)
    {
        foreach(ItemData item in list)
        {
            if (item.nameID.Equals(key))
            {
                return item;
            }
        }

        return null;
    }
}
