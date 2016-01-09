using UnityEngine;
using System.Collections;

// The script governs the behaviour of game paddle
// the paddle moves according to the mouse position

public class Paddle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//print (Input.mousePosition);	//print x, y, z coordinates
		// Input.mousePosition.x returns the x coordinate
		
		// print the relative x coordinate
		//print (Input.mousePosition.x / Screen.width);
		
		// f signifies float
		// maintains the y coordinate as defined in transform
		Vector3 paddlePos = new Vector3 (0.5f, this.transform.position.y, 0f);
		
		// relative x coordinate in game unit
		float mouseXPosInBlock = Input.mousePosition.x / Screen.width * 16;
		Debug.Log (mouseXPosInBlock);
		// update the paddle position
		paddlePos.x = Mathf.Clamp (mouseXPosInBlock, 0.5f, 15.5f);
		// this - the instance of the current Paddle script
		this.transform.position = paddlePos;
	}
}
