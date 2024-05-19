using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text roundsSurvivedLabel;

    private void OnEnable()
    {
        roundsSurvivedLabel.text = $"{PlayerStats.RoundsSurvived}";
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
