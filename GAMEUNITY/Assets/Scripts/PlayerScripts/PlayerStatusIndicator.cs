using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private RectTransform manaBarRect;

    //UI Camera Lock
    /*[SerializeField]
    private RectTransform UIhealthBarRect;
    [SerializeField]
    private RectTransform UImanaBarRect;
    [SerializeField]
    private Text UIhealthText;
    [SerializeField]
    private Text UImanaText;*/

    private void Start()
    {
        if (manaBarRect == null)
        {
            Debug.Log("STATUS INDICATOR: No mana bar");
        }
    }

    public void SetMana(int _cur, int _max)
    {
        float _value = (float)_cur / _max;
        manaBarRect.localScale = new Vector3(_value, 1f, 1f);

        //UImanaBarRect.localScale = new Vector3(_value, 1f, 1f);
        //UImanaText.text = _cur + "/" + _max + " MP";
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, 1f, 1f);

        //UIhealthBarRect.localScale = new Vector3(_value, 1f, 1f);
        //UIhealthText.text = _cur + "/" + _max + " HP";
    }


}
