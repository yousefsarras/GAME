using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class WeaponButton : MonoBehaviour {
	
	public PlayerManager player;
	public ShopItems shopStuff;
	public int itemNumber;
    public ItemBagUI itemBagUI;

    public Text name;
	public Text cost;
	public Text description;

	// Use this for initialization
	void Start () {
		SetButton ();
        if(itemBagUI == null)
        {
            GameObject.Find("ItemBagUI");
        }
	}

	void SetButton(){
		name.text = shopStuff.shopItems[itemNumber].name;
		cost.text = "$" + shopStuff.shopItems[itemNumber].cost.ToString();
		description.text = shopStuff.shopItems [itemNumber].description;
	}
		
	public void onClick(){
		if (player.playerStats.gold >= shopStuff.shopItems [itemNumber].cost) {
            Debug.Log("Entered the equipment");
			player.playerStats.gold -= shopStuff.shopItems [itemNumber].cost;
            //give players items in itembag
            int space = player.firstOpenSpace();

            player.itemBag[space] = shopStuff.shopItems[itemNumber];
            //add item icon in item UI
            GameObject inventorySlot = itemBagUI.itemSlots[space];
            //itemBagUI.itemSlots[space].GetComponent<Image>().GetComponentInChildren<Image>().sprite = shopStuff.shopItems[itemNumber].icon;
            inventorySlot.GetComponentInChildren<Image>().sprite = shopStuff.shopItems[itemNumber].icon;
            Debug.Log(itemBagUI.itemSlots[space].GetComponent<Image>());
            //add stats
            player.addStats(shopStuff.shopItems[itemNumber].aD, shopStuff.shopItems[itemNumber].defense, shopStuff.shopItems[itemNumber].health, shopStuff.shopItems[itemNumber].mana, shopStuff.shopItems[itemNumber].moveSpeed);
		} else {
			Debug.Log ("Not enough gold");
		}
	}
}

