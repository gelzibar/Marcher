using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PortlySage;

public class Prompt : MonoBehaviour
{
    private LevelController lc;
    string[] text;
    float timeMax, timeDelta;
    int indexCur;
    const int indexMax = 5;

    Text display;

    void Start()
    {
        OnStart();
    }

    void OnStart()
    {
        lc = GameObject.Find("Level Controller").GetComponent<LevelController>();
        indexCur = 0;
        text = new string[indexMax] { "Get Ready...", "3...", "2...", "1...", "Go!" };
        timeMax = 1.0f;
        timeDelta = 0.0f;

        display = gameObject.GetComponent<Text>();
        display.text = text[indexCur];
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
        if (indexCur < indexMax)
        {
            if (timeDelta > timeMax)
            {
                indexCur++;
                if (indexCur < indexMax)
                {
                    display.text = text[indexCur];
                }
                timeDelta = 0.0f;
            }
            else
            {
                timeDelta += Time.deltaTime;
            }
        }
        else
        {
            lc.SetCurState(States.Playable);
            gameObject.SetActive(false);
        }



    }
}
