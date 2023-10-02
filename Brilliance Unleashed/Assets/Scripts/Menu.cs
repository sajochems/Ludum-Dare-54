using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButton (int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void onQuitButton()
    {
        Application.Quit();
    }


}
