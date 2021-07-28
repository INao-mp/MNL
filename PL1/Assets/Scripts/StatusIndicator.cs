using UnityEngine.UI;
using UnityEngine;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthBarText;

    void Start()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("No stat bar");
        }
        if (healthBarText == null)
        {
            Debug.LogError("No stat bar");
        }
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthBarText.text = _cur + "/" + _max + "HP";

    }

}
