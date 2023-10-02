using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShop : MonoBehaviour
{

    private Transform container;
    private Transform shopItemTemplate;

    [SerializeField]
    GameObject player;

    PlayerUpgrade playerUpgrade;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("UpdateTemplate");
        shopItemTemplate.gameObject.SetActive(false);

        playerUpgrade = player.GetComponent<PlayerUpgrade>();
    }

    private void Start()
    {
        CreateItemButton(Updates.UpdateType.MoreSpeed, "More Speed", Updates.GetCost(Updates.UpdateType.MoreSpeed), 0);
        CreateItemButton(Updates.UpdateType.MoreInventory, "More Inventory", Updates.GetCost(Updates.UpdateType.MoreInventory), 1);
        CreateItemButton(Updates.UpdateType.AchieveWealth, "Achieve Wealth", Updates.GetCost(Updates.UpdateType.AchieveWealth), 2);

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
        int spendCoins = Updates.GetCost(updateType);
        if (TrySpendCoinsAmount(spendCoins))
        {
            switch (updateType)
            {
                default: Debug.Log("");
                    break;
                case Updates.UpdateType.MoreSpeed:
                    {
                        Debug.Log("I am a speedy boy now");
                        if (playerUpgrade.IncreaseSpeed())
                        {
                            CoinUIField.instance.AddCoins(-spendCoins);
                            Debug.Log("I am a speedy boy now");
                        } else
                        {
                            Debug.Log("Already fast af, boy");
                        }
                        break;
                    }                       
                    
                case Updates.UpdateType.MoreInventory:
                    {
                        if (playerUpgrade.IncreaseInventorySize())
                        {
                            CoinUIField.instance.AddCoins(-spendCoins);
                            Debug.Log("I can carry more stuff now");
                        } else
                        {
                            Debug.Log("Maximized stomach area");
                        }
                        
                        break;
                    }
                case Updates.UpdateType.AchieveWealth:
                    {
                        playerUpgrade.Victory();
                        Debug.Log("victory achieved");
                        break;
                    }
                    
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
