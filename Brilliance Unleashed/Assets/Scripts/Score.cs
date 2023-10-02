using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public float timeSpent = 0f;
    [SerializeField]
    TextMeshProUGUI scoreText;

    public bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        timeSpent = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            timeSpent += Time.deltaTime;
            scoreText.text = timeSpent.ToString();
        }
        
    }
}
