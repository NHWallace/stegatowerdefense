using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TMP_Text moneyLabel;

    private void Update()
    {
        moneyLabel.text = $"Money: ${PlayerStats.Money}";
    }
}
