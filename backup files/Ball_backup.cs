using UnityEngine;
using System.Collections;

// The script governs the behaviour of the ball in the game

public class Ball : MonoBehaviour {

	public Paddle game_Paddle;

	// distance between ball and paddle
	// it's a Vector3 variable
	private Vector3 paddleToBall_Vector;

	// Use this for initialization
	void Start () {
		// vertical distance between ball centre and paddle centre = 0.4f
		paddleToBall_Vector = this.transform.position - game_Paddle.transform.position;
	}

	// Update is called once per frame
	void Update () {
		this.transform.position = game_Paddle.transform.position + paddleToBall_Vector;
	}
}
