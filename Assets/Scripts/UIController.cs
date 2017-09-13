using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

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

	public void Button_ExitApp() {
		Application.Quit();
	}

	public void Button_StartGame() {
		SceneManager.LoadScene("Level");
	}
}
