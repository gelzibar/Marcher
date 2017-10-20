using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PortlySage;

public class Hazard : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    private LevelController lc;
    private Vector2 posNew;
    private float speedMax, speedMin, speedCur;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        lc = GameObject.Find("Level Controller").GetComponent<LevelController>();

        speedMax = 0.5f;
        speedMin = 0.02f;
        speedCur = 0.02f;

        posNew = rb.position;
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void OnFixedUpdate()
    {
        if (lc.GetCurState() == States.Playable)
        {
            float speedTemp = 0.0f;
            float absolute = Mathf.Abs(player.transform.position.y - transform.position.y);
            if (absolute < 5.0f)
            {
                speedTemp = (0.01f * lc.GetSpeed());
            }
            else
            {
                speedTemp = (absolute - 4.99f);
            }
            speedCur = speedTemp;



            posNew = new Vector2(rb.position.x, rb.position.y + speedCur);
            rb.MovePosition(posNew);
        }
    }

    void Update()
    {
        OnUpdate();
    }
    void OnUpdate()
    {

    }
}
