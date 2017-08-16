using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour {
	
	public PlayerManager player;
	public ShopItems shopStuff;
	public int itemNumber;

    public Text name;
	public Text cost;
	public Text description;

	// Use this for initialization
	void Start () {
		SetButton ();
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
			player.itemBag[player.firstOpenSpace()] = shopStuff.shopItems[itemNumber];
            player.addStats(shopStuff.shopItems[itemNumber].aD, shopStuff.shopItems[itemNumber].defense, shopStuff.shopItems[itemNumber].health, shopStuff.shopItems[itemNumber].mana, shopStuff.shopItems[itemNumber].moveSpeed);
		} else {
			Debug.Log ("Not enough gold");
		}
	}
}

