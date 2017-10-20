using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PortlySage;

public class Player : MonoBehaviour
{

    Rigidbody2D myrb;
    Animator myAnim;
    LevelController lc;
    bool leftInput, rightInput;
    int maxMoveCycle, curMoveCycle;
    bool isMoving, attemptMove;
    bool isAlive;
    float curDistance;
    bool leftPart, rightPart;

    private Vector2 posNew;
    float speedMax, speedMin, speedCur;
    public float speedMultiplied;

    float move, moveFactor, moveFactorY;

    void Awake()
    {
        OnAwake();
    }

    void OnAwake()
    {
        myrb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        lc = GameObject.Find("Level Controller").GetComponent<LevelController>();
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

        speedMax = 0.5f;
        speedMin = 0.02f;
        speedCur = 0.02f;
        speedMultiplied = speedCur * lc.GetSpeed();

        posNew = myrb.position;

        leftPart = false;
        rightPart = true;

    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    void OnFixedUpdate()
    {

        if (lc.GetCurState() == States.Playable)
        {
            float tempX = myrb.position.x;
            float tempY = myrb.position.y + speedMultiplied;
            if (isAlive)
            {
                if (attemptMove)
                {
                    tempX += move;
                    curMoveCycle++;
                }
                else if (!attemptMove)
                {
                    if (myrb.position.x % 1 != 0)
                    {
                        float target = Mathf.Round(myrb.position.x);
                        float resolution = Mathf.Lerp(myrb.position.x, target, moveFactor);
                        tempX = resolution;

                    }
                    // we want to identify the closest INT- So, if you're 3.6, then 4.
                    // and then establish a routine to move towards that INT.
                }
            }
            Vector2 vel = new Vector2(tempX, tempY);
            myrb.MovePosition(vel);

        }
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

        AnimationStateUpdate();

        if (isAlive)
        {
            posNew = new Vector2(myrb.position.x, myrb.position.y + speedCur);
            speedMultiplied = speedCur * lc.GetSpeed();
            if (!isMoving)
            {
                move = 0.0f;
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (lc.GetCurState() != States.Paused)
                {
                    lc.SetCurState(States.Paused);
                }
                else if (lc.GetCurState() == States.Paused)
                {
                    lc.SetCurState(States.Playable);
                }
            }
        }
        else
        {
            lc.SetCurState(States.GameOver);
        }
    }

    void AnimationStateUpdate()
    {

        if (myrb.position.y < 0.0f)
        {
            if (isAlive)
            {
                myAnim.SetBool("isRunning", true);
                myAnim.SetBool("isWalking", false);
                myAnim.SetBool("isIdle", false);
            }
        }
        else
        {
            if (isAlive)
            {
                myAnim.SetBool("isWalking", true);
                myAnim.SetBool("isRunning", false);
                myAnim.SetBool("isIdle", false);
            }
        }

        if (!isAlive)
        {
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isDying", true);
            myAnim.SetBool("isIdle", false);
        }

        if (lc.GetCurState() != States.Playable)
        {
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isDying", false);
            myAnim.SetBool("isIdle", true);
            transform.Find("Particle System 1").GetComponent<ParticleSystem>().Pause();
            transform.Find("Particle System 2").GetComponent<ParticleSystem>().Pause();
            transform.Find("Particle System 3").GetComponent<ParticleSystem>().Pause();
            transform.Find("Particle System 4").GetComponent<ParticleSystem>().Pause();
        }
        else
        {
            transform.Find("Particle System 1").GetComponent<ParticleSystem>().Play();
            transform.Find("Particle System 2").GetComponent<ParticleSystem>().Play();
        }

        if (isAlive && lc.GetCurState() == States.Playable)
        {
            if (move < 0)
            {
                if (!transform.Find("Particle System 3").GetComponent<ParticleSystem>().isPlaying)
                {
                    transform.Find("Particle System 3").GetComponent<ParticleSystem>().Play();
                }
            }
            else if (move > 0)
            {
                if (!transform.Find("Particle System 4").GetComponent<ParticleSystem>().isPlaying)
                {
                    transform.Find("Particle System 4").GetComponent<ParticleSystem>().Play();
                }
            }
            else
            {
                transform.Find("Particle System 3").GetComponent<ParticleSystem>().Stop();
                transform.Find("Particle System 4").GetComponent<ParticleSystem>().Stop();
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
            if (Vector2.Distance(collider.transform.position, myrb.position) < 0.5f)
            {
                curDistance = collider.gameObject.GetComponent<TileBG>().meters - 3.0f;
            }
        }
    }

    public float GetDistance()
    {
        return curDistance;
    }

    public bool GetAlive()
    {
        return isAlive;
    }

    public Vector2 GetPosition()
    {
        return myrb.position;
    }

}