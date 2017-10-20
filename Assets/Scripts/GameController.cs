using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	void Awake() {
		OnAwake();
	}

	void OnAwake() {
		#if UNITY_EDITOR
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
		#endif
	}
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
}