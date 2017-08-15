using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {

	public Transform cursor;

	void Start () {
		Instantiate (cursor);
	}

	void Update () {

		Vector3 mosPos = Input.mousePosition;
		cursor.position = mosPos;

	}
}
