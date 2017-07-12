using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;

    private void Start()
    {
        if(healthBarRect == null)
        {
            Debug.LogError("STATUS INDICATOR: No health bar");
        }
        if (healthText == null)
        {
            Debug.LogError("STATUS INDICATOR: No health text");
        }
    }

    //Updating to update heathBar
    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, 1f, 1f);
        healthText.text = _cur + "/" + _max + " HP";
    }

}
