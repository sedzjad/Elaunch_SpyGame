using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMenu : MonoBehaviour
{

    private float timeMinutes;
    private float timeSeconds;
    private float timer;

    private int caughts;

    private bool timeIsGoing;

    void Start()
    {
        timeIsGoing = true;
    }

    void Update()
    {
        if(timeIsGoing == true)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(timeIsGoing == true)
            {
                timeIsGoing = false;
            } else
            {
                timeIsGoing = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            caughts += 1;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {

            timeMinutes = Mathf.Floor(timer / 60);
            timeSeconds = (timer % 60);

            Debug.Log(timeMinutes.ToString("00")+ ":" + timeSeconds.ToString("00"));
        }
    }
}
