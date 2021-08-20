using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject buildEffect;
    public GameObject sellEffect;
    //public GameObject standartTurretPrefab;
    //public GameObject missileLauncherPrefab;

    private TurretBlueprint turretToBuild;
    private Cube selectedCube;

    public NodeUI nodeUI;

    public bool Canbuild {  get { return turretToBuild != null; } }
    public bool NotEnoughtMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public void SelectedNode(Cube cube)
    {
        if (selectedCube == cube)
        {
            DeselectedCube();
            return;
        }
        selectedCube = cube;
        turretToBuild = null;

        nodeUI.SetTarget(cube);
    }
    public void DeselectedCube()
    {
        selectedCube = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectedCube = null;

        nodeUI.Hide();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
