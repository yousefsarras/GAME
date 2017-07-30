using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAI : MonoBehaviour {

    public Transform target;
    public float movespeed = 3;
    Vector3 myPosition;

	void Start () {
        myPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetDirection = target.position - this.myPosition;
        myPosition.x += targetDirection.x * movespeed * Time.deltaTime;
        //Debug.Log("My minion position " + myPosition.x);
	}
}
