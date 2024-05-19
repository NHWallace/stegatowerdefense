using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint GunTower;
    public TowerBlueprint MissileTower;


    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectGunTower()
    {
        Debug.Log("Gun Tower Selected");
        buildManager.SelectTowerToBuild(GunTower);
    }
    public void SelecteMissileTower()
    {
        Debug.Log("Missile Tower Selected");
        buildManager.SelectTowerToBuild(MissileTower);
    }
}