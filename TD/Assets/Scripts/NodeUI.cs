using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Cube target;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    public void SetTarget(Cube _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

       

        if (!target.isUpgraded)
        {
            upgradeButton.interactable = true;
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
        }else
        {
            upgradeButton.interactable = false;
            upgradeCost.text = "Done";
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    { 
        target.UpgradeTurret();
        BuildManager.instance.DeselectedCube();
    }

    public void Sell()
    {
        target.SellTirret();
        BuildManager.instance.DeselectedCube();
    }
}
