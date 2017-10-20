using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PortlySage;

public class CameraController : MonoBehaviour
{

    public GameObject myPlayer;
    private Player playerScript;
    private Rigidbody2D rbPlayer;
    private Vector2 offsetPos;
    private Rigidbody2D rb;
    private LevelController lc;

    private const float threshold = 2.0f;
    private const float offset = 2.0f;


    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        rb = GetComponent<Rigidbody2D>();
        rbPlayer = myPlayer.GetComponent<Rigidbody2D>();
        playerScript = myPlayer.GetComponent<Player>();
        lc = GameObject.Find("Level Controller").GetComponent<LevelController>();

        offsetPos = new Vector2(0.0f, rbPlayer.position.y);

    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void OnFixedUpdate()
    {
        // if (lc.GetCurState() == States.Playable)
        // {
        //     offsetPos = new Vector2(0.0f, rb.position.y + playerScript.speedMultiplied);
        // }
        float tempY = 0.0f;
        if (rb.position.y < rbPlayer.position.y)
        {
            tempY = Mathf.Abs(rb.position.y - rbPlayer.position.y);
        }
        else
        {
            tempY = 0.0f;
        }
        offsetPos = new Vector2(0.0f, rb.position.y + tempY);
        rb.MovePosition(offsetPos);
    }

    void Update()
    {
        OnUpdate();
    }
    void OnUpdate()
    {
        if (lc.GetCurState() == States.Playable)
        {
            //if(Mathf.Abs(rb.position.y - (rbPlayer.position.y + offset)) > threshold) {
            //offsetPos = new Vector2(0.0f, rbPlayer.position.y + playerScript.speedMultiplied);
            //}
            // float tempY = 0.0f;
            // if (rb.position.y < rbPlayer.position.y)
            // {
            //     tempY = Mathf.Abs(rb.position.y - rbPlayer.position.y);
            // }else {
            // 	tempY = 0.0f;
            // }
            // offsetPos = new Vector2(0.0f, rb.position.y + tempY);
        }
    }
}
