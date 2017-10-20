using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSource : MonoBehaviour
{

    public GameObject pTile, pObstacle, pHazard;
    public GameObject myPlayer;
    private Player playerScript;
    private Vector2 playerPos, offsetPos;
    private float offset;

    private Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
        playerScript = myPlayer.GetComponent<Player>();
        offset = 10.0f;
        playerPos = playerScript.GetPosition();
        offsetPos = new Vector2(rb.position.x, playerPos.y + offset);
        

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
        hazardOffset = CalculateScreenHeight();

        PopulatePlaySpace();
        GenerateBG();

    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void OnFixedUpdate() {
        rb.MovePosition(offsetPos);

    }

    void Update()
    {
        OnUpdate();
    }
    void OnUpdate()
    {
        if (curTile != null)
        {
            if (Vector3.Distance(transform.position, curTile.transform.position) > 1.0f)
            {
                if (Random.Range(0, 20) >= obstaclePercent)
                {
                    GenerateObstacle();
                }
                GenerateBG();
            }
        }

        playerPos = playerScript.GetPosition();
        offsetPos = new Vector2(rb.position.x, playerPos.y + offset);


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
                    Vector3 pos = new Vector3(location.x, transform.position.y - (i+1), location.z);
                    GenerateHazard(pos);
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

    int CalculateScreenHeight() {
        int height = Screen.height;
        int tileSize = 64;

        return height / tileSize;

    }
}
