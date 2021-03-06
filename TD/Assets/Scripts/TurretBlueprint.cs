using UnityEngine;

[System.Serializable]
public class TurretBlueprint 
{
    public GameObject prefabs;
    public int cost;

    public GameObject upgradedPrefabs;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
