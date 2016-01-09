using UnityEngine;
using System.Collections;

// The script governs the game results and calls the
// Level Manager when the game ends

public class LoseCollider : MonoBehaviour {

	private LevelManager level_Manager;
	
	void OnTriggerEnter2D (Collider2D ball_collider) {
		level_Manager = GameObject.FindObjectOfType<LevelManager>();
		level_Manager.LoadLevel("Lose");
	}

	void OnCollisionEnter2D (Collision2D ball_collision) {
		Debug.Log ("Collision");
	}
}
