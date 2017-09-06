using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
	public Sprite[] background;

	void Start () {
		OnStart();
	}

	void OnStart() {
	}
	
	void FixedUpdate() {
		OnFixedUpdate();
	}

	void OnFixedUpdate() {}

	void Update () {
		OnUpdate();
	}
	void OnUpdate() {

	}

	public Sprite RandomBackground() {
		int index = Random.Range(0, background.Length);
		return background[index];
	}
}
