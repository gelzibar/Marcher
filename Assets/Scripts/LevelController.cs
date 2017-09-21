using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	float playerDistance;
	float baseSpeed;
	float multiplierSpeed;
	float curSpeed;

	public Player myPlayer;

	void Start () {
		OnStart();
	}

	void OnStart() {

		playerDistance = myPlayer.GetDistance();
		baseSpeed = 0.1f;
		multiplierSpeed = 1.0f;
		curSpeed = baseSpeed * multiplierSpeed;
	}
	
	void FixedUpdate() {
		OnFixedUpdate();
	}

	void OnFixedUpdate() {}

	void Update () {
		OnUpdate();
	}

	void OnUpdate() {
		playerDistance = myPlayer.GetDistance();
		multiplierSpeed = (playerDistance / 100.0f);
		curSpeed = baseSpeed * multiplierSpeed;
	}

	public float GetSpeed() {
		return curSpeed;
	}

}
