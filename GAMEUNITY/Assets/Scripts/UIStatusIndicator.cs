using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusIndicator : MonoBehaviour {

    //UI Camera Lock
    [SerializeField]
    private RectTransform UIhealthBarRect;
    [SerializeField]
    private RectTransform UImanaBarRect;
    [SerializeField]
    private Text UIhealthText;
    [SerializeField]
    private Text UImanaText;

    private void Start()
    {
        if (UIhealthBarRect == null)
        {
            Debug.LogError("STATUS INDICATOR: No health bar");
        }
        if (UIhealthText == null)
        {
            Debug.LogError("STATUS INDICATOR: No health text");
        }
    }

    //Updating to update heathBar
    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        UIhealthBarRect.localScale = new Vector3(_value, 1f, 1f);
        UIhealthText.text = _cur + "/" + _max + " HP";
    }

    public void SetMana(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        UImanaBarRect.localScale = new Vector3(_value, 1f, 1f);
        UImanaText.text = _cur + "/" + _max + " MP";
    }
}
