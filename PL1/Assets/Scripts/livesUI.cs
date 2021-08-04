using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class livesUI : MonoBehaviour
{
    [SerializeField]
    private Text livesText;

    void Awake()
    {
        livesText = GetComponent<Text>();    
    }

    // Update is called once per frame
    void Update()
    {
       livesText.text ="Lives : " + GM_Main.RemainingLives.ToString();
    }
}
