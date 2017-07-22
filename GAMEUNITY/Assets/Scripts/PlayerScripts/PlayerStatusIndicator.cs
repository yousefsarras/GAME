using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private RectTransform manaBarRect;
    [SerializeField]
    private Text levelText;

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
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, 1f, 1f);
    }

    public void SetLevel(int _cur)
    {
        levelText.text = _cur.ToString();
    }


}
