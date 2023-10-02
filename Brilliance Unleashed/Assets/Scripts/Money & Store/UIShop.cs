using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShop : MonoBehaviour
{

    private Transform container;
    private Transform shopItemTemplate;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("UpdateTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(Updates.UpdateType.MoreSpeed, "More Speed", Updates.GetCost(Updates.UpdateType.MoreSpeed), 0);
        CreateItemButton(Updates.UpdateType.MoreInventory, "More Inventory", Updates.GetCost(Updates.UpdateType.MoreInventory), 1);

        Hide();
    }

    private void CreateItemButton(Updates.UpdateType updateType, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 150f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);


        shopItemTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemCost + "");
        shopItemTransform.Find("NameText").GetComponentInChildren<TextMeshProUGUI>().text = itemName;

        shopItemTransform.gameObject.SetActive(true);

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(updateType));
    }

    public void TryBuyItem(Updates.UpdateType updateType)
    {
        if(TrySpendCoinsAmount(Updates.GetCost(updateType)))
        {
            switch (updateType)
            {
                default: Debug.Log("");
                    break;
                case Updates.UpdateType.MoreSpeed: Debug.Log("I am a speedy boy now");
                    break;
                case Updates.UpdateType.MoreInventory: Debug.Log("I can carry more stuff now");
                    break;
            }
            
        } else
        {
            Debug.Log("Your money is practicing social distancing from your desires. You don't have " + Updates.GetCost(updateType));
        }
    }

    public bool  TrySpendCoinsAmount(int spendCoins)
    {
        int price = CoinUIField.instance.GetCoins();

        if(price >= spendCoins)
        {
            CoinUIField.instance.AddCoins(-spendCoins);
            return true;
        } else return false;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
