using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField] TMP_Text livesLabel;

    private void Update()
    {
        livesLabel.text = $"Lives: {PlayerStats.Lives}";
    }
}
