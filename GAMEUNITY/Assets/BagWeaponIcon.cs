using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagWeaponIcon : MonoBehaviour {

    public bool itemSelected = false;

	// Use this for initialization
	void Start () {
		
	}
	
    public void onClick()
    {
        itemSelected = !itemSelected;
        Debug.Log("Item Selected: " + itemSelected);
    }
	// Update is called once per frame
	void Update () {

        // if(itemSelected == true &&)
	}
}
