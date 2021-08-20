using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer ren;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        ren = GetComponent<Renderer>();
        startColor = ren.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectedNode(this);
            return;
        }

        if (!buildManager.Canbuild)
            return;

        Buildturret(buildManager.GetTurretToBuild());
    }

    void Buildturret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enought Money");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefabs, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build!" + PlayerStats.Money);
    }

    public void UpgradeTurret()
    {
        
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enought Money to upgrade");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        
        //destroy old
        Destroy(turret);

        //build new one

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefabs, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");
    }

    public void SellTirret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.Canbuild)
            return;

        if (buildManager.NotEnoughtMoney)
        {
            ren.material.color = hoverColor;
        }else
        {
            ren.material.color = notEnoughColor;
        }
        
    }

    void OnMouseExit()
    {
        ren.material.color = startColor;
    }
}
