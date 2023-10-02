using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUIField : MonoBehaviour
{
    public static CoinUIField instance;

    public TextMeshProUGUI coinText;
    private int currentCoins = 45;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = currentCoins.ToString();
    }

    // Start is called before the first frame update
    public void AddCoins(int amount)
    {
        currentCoins += amount;
        coinText.text = currentCoins.ToString();
    }

    public int GetCoins()
    {
        return currentCoins;
    }
}
