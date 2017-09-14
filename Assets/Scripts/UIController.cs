using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public GameObject distance, distance2;
	public GameObject deathUI;
    public GameObject player;
    public Player playerScript;
    private Text distanceText, distanceText2;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        distanceText = distance.GetComponent<Text>();
        distanceText2 = distance2.GetComponent<Text>();
        playerScript = player.GetComponent<Player>();
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void OnFixedUpdate() { }

    void Update()
    {
        OnUpdate();
    }
    void OnUpdate()
    {
        distanceText.text = playerScript.GetDistance().ToString() + "m";
        distanceText2.text = playerScript.GetDistance().ToString() + "m";

		if(!playerScript.GetAlive()) {
			deathUI.SetActive(true);
		}else {
			deathUI.SetActive(false);
		}
    }

    public void Button_ExitApp()
    {
        Application.Quit();
    }

    public void Button_StartGame()
    {
        SceneManager.LoadScene("Level");
    }

	public void Button_MainMenu() {
		SceneManager.LoadScene("Menu");
	}
}
