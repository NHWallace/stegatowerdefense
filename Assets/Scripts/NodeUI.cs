using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    [SerializeField] TMP_Text upgradeLabel;
    public Button upgradeButton;

    [SerializeField] TMP_Text sellLabel;


    private Node target;

    public void SetTarget(Node target)
    {
        this.target = target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeLabel.text = $"${target.towerBlueprint.upgradeCost}";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeLabel.text = ("DONE!");
            upgradeButton.interactable = false;
        }

        sellLabel.text = $"${target.towerBlueprint.GetSellAmount()}";

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }
}
