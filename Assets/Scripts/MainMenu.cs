using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad = "Level1";
    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Settings()
    {
        Debug.Log("Settings Clicked");
    }

    public void Quit()
    {
        Debug.Log("Exiting.");
        Application.Quit(); // Only works in a built version, not in the editor
    }
}