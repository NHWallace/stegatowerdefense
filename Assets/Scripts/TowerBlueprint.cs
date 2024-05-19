using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // This allows unity to save and load these values
public class TowerBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost * 8 / 10;
    }
}