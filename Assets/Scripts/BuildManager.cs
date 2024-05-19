using UnityEngine;
using UnityEngine.Pool;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance; // Required to enforce singleton pattern - only 1 instance allowed

    public GameObject buildEffect;

    private TowerBlueprint towerToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
        }
        instance = this;
    }

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;

        DeselectNode();
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
