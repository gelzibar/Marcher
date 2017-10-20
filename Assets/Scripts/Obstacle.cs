using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	TileManager myTileManager;
	SpriteRenderer mySR;
	LevelController myLevel;

	void Start () {
		OnStart();
	}

	void OnStart() {
		myLevel = GameObject.Find("Level Controller").GetComponent<LevelController>();
	}
	
	void FixedUpdate() {
		OnFixedUpdate();
	}

	void OnFixedUpdate() {
	}

	void Update () {
		OnUpdate();
	}
	void OnUpdate() {

	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Outbounds") {
			Destroy(gameObject);
		}
	}
}
