using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellSwitch : MonoBehaviour {

    public bool sellSwitch;

    public void Start()
    {
        sellSwitch = false;
    }

    public void OnClick()
    {
        sellSwitch = !sellSwitch;
    }
}
