﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// This script a the game's level manager
// It manages the flow of different game scenes

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log("Level load requested for: "+name);
        // reset breakable brick count
        Brick.breakable_Count = 0;
        // name could be any scene in the game
        //Application.LoadScene(name); obsolete function

        SceneManager.LoadScene(name);
	}

	public void QuitRequest() {
		Debug.Log ("Quit requested to leave game");
		// no effect for debug and web build
		Application.Quit ();
	}
	
	public void LoadNextLevel() {
		// reset breakable brick count
		Brick.breakable_Count = 0;
        //Application.LoadLevel (Application.loadedLevel + 1); obsolete

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	public void BrickDestroyed() {
		// all bricks are destroyed
		if (Brick.breakable_Count <= 0) {
			LoadNextLevel();
		}
	}
}
