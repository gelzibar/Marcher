using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	float playerDistance;
	float baseSpeed;
	float multiplierSpeed;
	float curSpeed;

	public Player myPlayer;

	bool isActive, isPaused;

	void Start () {
		OnStart();
	}

	void OnStart() {

		playerDistance = myPlayer.GetDistance();
		baseSpeed = 0.1f;
		multiplierSpeed = 1.0f;
		curSpeed = baseSpeed * multiplierSpeed;

		isActive = true;
		isPaused = false;
	}
	
	void FixedUpdate() {
		OnFixedUpdate();
	}

	void OnFixedUpdate() {}

	void Update () {
		OnUpdate();
	}

	void OnUpdate() {
		isActive = myPlayer.GetAlive();

		if(isActive && !isPaused) {
		playerDistance = myPlayer.GetDistance();
		multiplierSpeed = (playerDistance / 100.0f);
		curSpeed = baseSpeed * multiplierSpeed;
		} else {
			curSpeed = 0.0f;
		}
	}

	public float GetSpeed() {
		return curSpeed;
	}

	public bool GetActive() {
		return isActive;
	}

	public bool GetPaused() {
		return isPaused;
	}

	public void TogglePause() {
		isPaused = !isPaused;
	}

}
