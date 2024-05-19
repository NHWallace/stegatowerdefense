using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameEnded;

    public GameObject GameOverUI;

    private void Start()
    {
        GameEnded = false;
    }

    private void Update()
    {
        if (GameEnded)
        {
            return;
        }

        if (PlayerStats.Lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameEnded = true;
        GameOverUI.SetActive(true);
    }
}
