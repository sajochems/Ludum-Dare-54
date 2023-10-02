using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUIField : MonoBehaviour
{
    public static CoinUIField instance;

    public TextMeshProUGUI coinText;
    public int currentCoins = 0;

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
    void AddCoins(int amount)
    {
        currentCoins += amount;
        coinText.text = currentCoins.ToString();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddCoins(1);
            Debug.Log(currentCoins);
        }
    }
}
