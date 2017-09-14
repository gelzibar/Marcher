using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D myrb;
    Animator myAnim;
    bool leftInput, rightInput;
    int maxMoveCycle, curMoveCycle;
    bool isMoving, attemptMove;
    bool isAlive;
    float curDistance;


    float move, moveFactor, moveFactorY;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        myrb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        leftInput = false;
        rightInput = false;

        maxMoveCycle = 10;
        curMoveCycle = 0;
        isMoving = false;
        attemptMove = false;
        isAlive = true;
        move = 0.0f;
        moveFactor = 0.1f;
        moveFactorY = 0.01f;

        curDistance = 0.0f;
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void OnFixedUpdate()
    {
        float yGoal = myrb.position.y;
        Vector2 vel = new Vector2(myrb.position.x, yGoal);

        if (myrb.position.y < 0.0f)
        {
            float target = 0.0f;
            float resolution = myrb.position.y + moveFactorY;
            if (isAlive)
            {
                yGoal = resolution;
                vel = new Vector2(myrb.position.x, yGoal);
                myAnim.SetBool("isRunning", true);
                myAnim.SetBool("isWalking", false);
            }
        }
        else
        {
            if (isAlive)
            {
                myAnim.SetBool("isWalking", true);
                myAnim.SetBool("isRunning", false);
            }
        }

        if (!isAlive)
        {
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isDying", true);
        }

        if (isAlive)
        {
            if (attemptMove)
            {
                vel = new Vector2(myrb.position.x + move, yGoal);
                curMoveCycle++;
            }
            else if (!attemptMove)
            {
                if (myrb.position.x % 1 != 0)
                {
                    float target = Mathf.Round(myrb.position.x);
                    float resolution = Mathf.Lerp(myrb.position.x, target, moveFactor);
                    //resolution = Mathf.Clamp(resolution, 0.0f, 0.1f);
                    //Debug.Log(target + " : " + resolution);
                    vel = new Vector2(resolution, yGoal);

                }
                // we want to identify the closest INT- So, if you're 3.6, then 4.
                // and then establish a routine to move towards that INT.
            }
        }
        myrb.MovePosition(vel);

        myrb.velocity = Vector2.zero;
    }

    void Update()
    {
        OnUpdate();
    }
    void OnUpdate()
    {

        leftInput = false;
        rightInput = false;

        if (isAlive)
        {
            if (!isMoving)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    leftInput = true;
                    isMoving = true;
                    move = moveFactor * -1;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    rightInput = true;
                    isMoving = true;
                    move = moveFactor;
                }
            }

            if (isMoving)
            {
                if (curMoveCycle < maxMoveCycle)
                {
                    attemptMove = true;
                }
                else if (curMoveCycle >= maxMoveCycle)
                {
                    attemptMove = false;
                    curMoveCycle = 0;
                    isMoving = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Hazard")
        {
            isAlive = false;
            // Game Fail screen pops up
        }

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Background")
        {
            if (Vector2.Distance(collider.GetComponent<Rigidbody2D>().position, myrb.position) < 0.5f)
            {
                curDistance = collider.gameObject.GetComponent<TileBG>().meters;
            }
        }
    }

    public float GetDistance()
    {
        return curDistance;
    }

    public bool GetAlive() {
        return isAlive;
    }

}
