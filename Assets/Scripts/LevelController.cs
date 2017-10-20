using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PortlySage;

public class LevelController : MonoBehaviour
{

    float playerDistance;
    public float baseSpeed;
    public float multiplierSpeed;
    public float curSpeed;

    public Player myPlayer;
    public AudioSource myAudioTracks;
    public AudioClip gameoverTrack, themeTrack;

    States curState;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        curState = States.Init;
        playerDistance = myPlayer.GetDistance();
        baseSpeed = 1.0f;
        multiplierSpeed = 1.0f;
        curSpeed = baseSpeed * multiplierSpeed;
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
        if (curState == States.Playable)
        {
            if (!myAudioTracks.isPlaying)
            {
                myAudioTracks.clip = themeTrack;
                myAudioTracks.Play();
            }
            playerDistance = myPlayer.GetDistance();
            multiplierSpeed = 1.0f + (playerDistance / 50.0f);
            multiplierSpeed = Mathf.Clamp(multiplierSpeed, 1.0f, 6.0f);
            curSpeed = baseSpeed * multiplierSpeed;
        }
        else if (curState == States.GameOver)
        {
            if (myAudioTracks.clip == themeTrack)
            {
                myAudioTracks.clip = gameoverTrack;
                myAudioTracks.Play(22050);
            }
        }
    }

    public float GetSpeed()
    {
        return curSpeed;
    }

    public States GetCurState()
    {
        return curState;
    }

    public void SetCurState(States state)
    {
        curState = state;
    }

}
