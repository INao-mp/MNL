using UnityEngine;
using UnityEngine.UI;

public class LevelStatus : MonoBehaviour
{
    public static LevelStatus instance { get; private set; }

    [SerializeField] private Text text;
    [HideInInspector]
    public int upLevel = 1;
    private int level = 1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
    }

    public void UpLevel()
    {
        level = upLevel;
        upLevel++;
    }

    public int Asd()
    {
        if (upLevel > level)
        {
            text.text = upLevel.ToString();
            return upLevel;
        }
        return level;
    }
}
