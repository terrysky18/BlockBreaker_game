using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public AudioClip crack_Sound;
	public Sprite[] hit_Sprites;
	public GameObject smoke;
	
	// static variable should not be initialised in Start()
	public static int breakable_Count = 0;
	private int times_Hit;
	private LevelManager level_manager;
	private bool is_Breakable;
	private bool debugging = true;

	// Use this for initialization
	void Start () {
		times_Hit = 0;
		is_Breakable = (this.tag == "Breakable");
		
		// keep track of breakable bricks
		if (is_Breakable) {
			breakable_Count++;
		}
		if (debugging) {
			Debug.Log ("Total breakable bricks = "+breakable_Count);
		}
		level_manager = GameObject.FindObjectOfType<LevelManager>();
	}

	// Update is called once per frame
	void Update () {
	}

    //void OnCollisionEnter2D (Collision2D brick_collision) {
    // OnCollisionExit2D is needed for Unity 5.3
    void OnCollisionExit2D(Collision2D brick_collision) {
        if (is_Breakable) {
			HandleHits();
			// play the sound effect at the brick position
			AudioSource.PlayClipAtPoint(crack_Sound, transform.position, 1f);
		}
	}

	void HandleHits () {
		//Debug.Log ("Brick hit");
		times_Hit++;
		int max_Hits = hit_Sprites.Length + 1;
		
		if (times_Hit >= max_Hits) {
			// a brick is destroyed
			breakable_Count--;
			Destroy(gameObject);
			// tell LevelManager a brick is destroyed
			level_manager.BrickDestroyed();

			// create a puff of dust
			PuffSmoke();

			if (debugging) {
				Debug.Log("Total breakable bricks: "+breakable_Count);
			}
		}
		else {
			// load the sprite image in hit_sprites array
			LoadSprites();
		}
	}

	void LoadSprites () {
		// hit_sprites array index
		int sprite_Index = times_Hit - 1;
		// check if the element in hit_sprites exists
		if (hit_Sprites[sprite_Index]) {
			// tell the Sprite Render to load a specific sprite
			this.GetComponent<SpriteRenderer>().sprite = hit_Sprites[sprite_Index];
		}
		else {
			Debug.LogError ("Sprite image not found");
		}
	}
	
	void PuffSmoke () {
		// create a puff of dust
		GameObject dust_puff;
		dust_puff = Instantiate(smoke, this.transform.position, Quaternion.identity) as GameObject;
		// change the dust colour
		dust_puff.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
	}

	public bool IsBreakable () {
		return is_Breakable;
	}
	
	// TODO:  REMOVE THIS FUNCTION ONCE GAME CAN BE WON
	void SimulateWin () {
		level_manager.LoadNextLevel();
	}
}
