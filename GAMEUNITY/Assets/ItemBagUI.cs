using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBagUI : MonoBehaviour {

    public GameObject itemBag;
    bool shopOpened = false;
    public GameObject[] itemSlots;

    public void OpenBag() {
        itemBag.SetActive(true);
        shopOpened = true;
    }

    public void CloseBag()
    {
        itemBag.SetActive(false);
        shopOpened = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(shopOpened == true)
            {
                CloseBag();
            }
            else
            {
                OpenBag();
            }
        }
    }
}
