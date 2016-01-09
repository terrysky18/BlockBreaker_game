using UnityEngine;
using System.Collections;

// The script governs the behaviour of the ball in the game

public class Ball : MonoBehaviour {

	private LongPaddle game_Paddle;

	// distance between ball and paddle
	// it's a Vector3 variable
	private Vector3 paddleToBall_Vector;
	
	// rotates the ball
	private float rotation_angle = 5.0f;

	// check whether game has begun
	private bool game_Begun = false;
	
	// Use this for initialization
	void Start () {
		game_Paddle = GameObject.FindObjectOfType<LongPaddle>();

		// vertical distance between ball centre and paddle centre = 0.4f
		paddleToBall_Vector = this.transform.position - game_Paddle.transform.position;
	}

	// Update is called once per frame
	void Update () {
		// rotate the ball about Z axis
		this.transform.Rotate(Vector3.forward, rotation_angle);
		
		if (!game_Begun) {
			// lock the ball relative to the paddle
			this.transform.position = game_Paddle.transform.position + paddleToBall_Vector;

			// listen to mouse left click and respond
			if (Input.GetMouseButtonDown(0)) {
				Debug.Log ("Left click detected, launch ball");
				game_Begun = true;
				this.GetComponent<Rigidbody2D>().velocity = new Vector2 (2f, 10f);
			}
		}
		//Debug.Log(this.rigidbody2D.velocity);
		//Debug.Log(this.rigidbody2D.velocity.magnitude);
	}
	
	// the ball hits something
	void OnCollisionEnter2D (Collision2D ball_collision) {
		//play a sound effect
		if (game_Begun) {
			GetComponent<AudioSource>().Play ();

			//Debug.Log(this.GetComponent<Rigidbody2D>().velocity.magnitude);
			// subtle change to the ball's velocity within playable limit
			if (this.GetComponent<Rigidbody2D>().velocity.magnitude <= 18f) {
				//Debug.Log("boost");
				this.GetComponent<Rigidbody2D>().velocity += VeloctiyBoost();
			}
			else if (this.GetComponent<Rigidbody2D>().velocity.magnitude >=19f){
				//Debug.Log("drag");
				this.GetComponent<Rigidbody2D>().velocity += VeloctiyDrag();
			}
		}
	}
	
	// return a Vector2 variable
	Vector2 VeloctiyBoost() {
		// subtle boost in ball velocity
		float hori_comp = Mathf.Sign(this.GetComponent<Rigidbody2D>().velocity.x)*Random.Range(-0.1f, 0.1f);
		float vert_comp = Mathf.Sign(this.GetComponent<Rigidbody2D>().velocity.y)*Random.Range(0f, 0.1f);
		Vector2 v_boost = new Vector2(hori_comp, vert_comp);
		//Debug.Log (v_boost);
		return v_boost;
	}
	
	// return a Vector2 variable
	Vector2 VeloctiyDrag() {
		// subtle boost in ball velocity
		// opposite direction, a quarter of the magnitude
		float hori_comp = -0.25f*Mathf.Sign(this.GetComponent<Rigidbody2D>().velocity.x);
		float vert_comp = -0.25f*Mathf.Sign(this.GetComponent<Rigidbody2D>().velocity.y);
		Vector2 v_drag = new Vector2(hori_comp, vert_comp);
		//Debug.Log (v_drag);
		return v_drag;
	}
}
