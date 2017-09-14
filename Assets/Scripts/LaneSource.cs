using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSource : MonoBehaviour
{

    public GameObject pTile, pObstacle, pHazard;
    public bool obstacleOnly;
    private GameObject curTile;
    private int obstaclePercent;
    private int hazardOffset;
    private float curDistance;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        curDistance = 0;
        if (obstacleOnly)
        {
            obstaclePercent = 0;
            GenerateObstacle();
        }
        else
        {
            obstaclePercent = 18;
        }
        hazardOffset = 15;

        PopulatePlaySpace();
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
                if (Random.Range(0, 20) >= obstaclePercent)
                {
                    GenerateObstacle();
                }
                GenerateBG();
            }
        }


    }

    void PopulatePlaySpace()
    {
        int numRows = 11;

        for (int i = numRows; i >= 0; i--)
        {
            Vector3 location = new Vector3(transform.position.x, transform.position.y - (i + 1), transform.position.z);
            GenerateBG_At(location);
            if (obstacleOnly)
            {
                GenerateObstacle_At(location);
            }
            else
            {
                if (i == numRows)
                {
                    GenerateHazard(location);
                }
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
        Debug.Log("BG Executing");
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
        curDistance++;
        curTile.GetComponent<TileBG>().SetDistance(curDistance);
    }

    void GenerateBG_At(Vector3 location)
    {

        curTile = Instantiate(pTile, location, transform.rotation, GameObject.Find("Background").transform);
        curDistance++;
        curTile.GetComponent<TileBG>().SetDistance(curDistance);

        // Since 'curTile' won't properly be represented by this bulk generation, clear it.
        curTile = null;
    }

    void GenerateObstacle_At(Vector3 location)
    {
        Instantiate(pObstacle, location, transform.rotation, GameObject.Find("Obstacles").transform);
    }

    void GenerateHazard(Vector3 location)
    {
        Instantiate(pHazard, location, transform.rotation, GameObject.Find("Hazards").transform);
    }
}
