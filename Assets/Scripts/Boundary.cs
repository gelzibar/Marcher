using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

	public GameObject myPlayer;
	private Player playerScript;
	private Vector2 playerPos, offsetPos;
    private float offset;
	private Rigidbody2D rb;

	void Start () {
		OnStart();
	}

	void OnStart() {
		rb = GetComponent<Rigidbody2D>();

		playerScript = myPlayer.GetComponent<Player>();
		playerPos = playerScript.GetPosition();
		offset = -10.0f;
		offsetPos = new Vector2(rb.position.x, rb.position.y + offset);
	}
	
	void FixedUpdate() {
		OnFixedUpdate();
	}

	void OnFixedUpdate() {
		rb.MovePosition(offsetPos);
	}

	void Update () {
		OnUpdate();
	}
	void OnUpdate() {
		playerPos = playerScript.GetPosition();
		offsetPos = new Vector2(rb.position.x, playerPos.y + offset);

	}
}
