using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money; // Note: Static variables carry values between scenes
    public int startMoney = 800;

    public static int Lives;
    public int startLives = 5;

    public static int RoundsSurvived;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;

        RoundsSurvived = 0;
    }


}