using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Renderer rendererReference;
    private Color startColor;
    public Color hovercolor;
    public Color noMoneyColor;

    public Vector3 spawnPositionOffset;

    BuildManager buildManager;
    [SerializeField] private AudioClip buildSound;
    [SerializeField] private AudioClip sellSound;

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerBlueprint towerBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private void Start()
    {
        rendererReference = GetComponent<Renderer>();
        startColor = rendererReference.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + spawnPositionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (tower != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTower(buildManager.GetTowerToBuild());
    }

    void BuildTower (TowerBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        GameObject tower = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        this.tower = tower;

        towerBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        SoundFXManager.instance.PlaySoundFXClip(buildSound, transform, SoundFXManager.instance.EffectVolume);

        Debug.Log($"Tower built, Money Left: {PlayerStats.Money}");
    }

    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= towerBlueprint.upgradeCost;

        // Get rid of the old tower
        Destroy(this.tower);

        // Build the new upgraded tower
        GameObject tower = Instantiate(towerBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        this.tower = tower;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        SoundFXManager.instance.PlaySoundFXClip(buildSound, transform, SoundFXManager.instance.EffectVolume);

        isUpgraded = true;

        Debug.Log($"Tower upgraded, Money Left: {PlayerStats.Money}");
    }

    public void SellTower()
    {
        PlayerStats.Money += towerBlueprint.GetSellAmount();

        // Could add a sell effect here
        SoundFXManager.instance.PlaySoundFXClip(sellSound, transform, SoundFXManager.instance.EffectVolume);

        Destroy(tower);
        towerBlueprint = null;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rendererReference.material.color = hovercolor;
        }
        else
        {
            rendererReference.material.color = noMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rendererReference.material.color = startColor;
    }
}
