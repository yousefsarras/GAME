using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

	public GameObject shopCanvas;

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.CompareTag ("Player")) {
			OpenShop ();
		}
	}

	public void OpenShop(){
		shopCanvas.SetActive (true);
	}


	public void CloseShop(){
		shopCanvas.SetActive (false);
	}
}
