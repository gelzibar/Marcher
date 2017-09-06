﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSource : MonoBehaviour
{

    public GameObject pTile, pObstacle;
    private GameObject curTile;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        GenerateBG();
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
        if (curTile != null)
        {
            if (Vector3.Distance(transform.position, curTile.GetComponent<Rigidbody2D>().position) > 1.0f)
            {
                if (Random.Range(0, 20) >= 18)
                {
                    GenerateObstacle();
                }
                GenerateBG();
            }
        }


    }

    void GenerateTile(GameObject template, Transform parent)
    {
        Vector3 location = new Vector3();
        if (curTile != null)
        {
            location = new Vector3(transform.position.x, curTile.transform.position.y + 1.0f, transform.position.z);
        }
        else
        {
            location = transform.position;
        }

        curTile = Instantiate(template, location, transform.rotation, parent);
    }

    void GenerateObstacle()
    {
        Vector3 location = new Vector3();
        if (curTile != null)
        {
            location = new Vector3(transform.position.x, curTile.transform.position.y + 1.0f, transform.position.z);
        }
        else
        {
            location = transform.position;
        }

		Instantiate(pObstacle, location, transform.rotation, GameObject.Find("Obstacles").transform);
    }

    void GenerateBG()
    {
        Vector3 location = new Vector3();
        if (curTile != null)
        {
            location = new Vector3(transform.position.x, curTile.transform.position.y + 1.0f, transform.position.z);
        }
        else
        {
            location = transform.position;
        }

        curTile = Instantiate(pTile, location, transform.rotation, GameObject.Find("Background").transform);
    }
}
