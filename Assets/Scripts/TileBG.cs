using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBG : MonoBehaviour
{

    TileManager myTileManager;
    SpriteRenderer mySR;
    Rigidbody2D myRB;
    LevelController myLevel;
    public float meters;
    float curSpeed;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        myTileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
        mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = myTileManager.RandomBackground();

        myRB = GetComponent<Rigidbody2D>();

        myLevel = GameObject.Find("Level Controller").GetComponent<LevelController>();

        curSpeed = myLevel.GetSpeed();

    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void OnFixedUpdate()
    {
        Vector2 movement = new Vector2(myRB.position.x, myRB.position.y + - curSpeed);
        myRB.MovePosition(movement);

    }

    void Update()
    {
        OnUpdate();
    }
    void OnUpdate()
    {

        curSpeed = myLevel.GetSpeed();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Outbounds")
        {
            Destroy(gameObject);
        }
    }

    public void SetDistance(float dist)
    {
        meters = dist;
    }
}
