using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBG : MonoBehaviour
{

    TileManager myTileManager;
    SpriteRenderer mySR;
    LevelController myLevel;
    public float meters;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        myTileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
        mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = myTileManager.RandomBackground();

        myLevel = GameObject.Find("Level Controller").GetComponent<LevelController>();

    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void OnFixedUpdate()
    {

    }

    void Update()
    {
        OnUpdate();
    }
    void OnUpdate()
    {

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
