using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public TurretBlueprint standartTurrer;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint LaserBeamer;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void selectStandartTurret ()
    {
        Debug.Log("Standart Turret Purchase");
        buildManager.SelectTurretToBuild(standartTurrer);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Purchase");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser beamer Purchase");
        buildManager.SelectTurretToBuild(LaserBeamer);
    }
}
