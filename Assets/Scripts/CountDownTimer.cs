using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer
{
    private float countDownTime;
    private float time;
    private bool resetOnZero = true;
    private bool paused = false;

    public CountdownTimer(float cCountDownTime, bool cResetOnZero = true) 
    {
        countDownTime = cCountDownTime;
        resetOnZero = cResetOnZero;
        time = countDownTime;
    }

    public bool CountDown()
    {
        if (paused) return false;
        if (time > 0)
        {
            time -= Time.deltaTime;
            return false;
        }
        if (resetOnZero) Reset();
        return true;
    }

    public void Reset()
    {
        time = countDownTime;
    }

    public void SetCountdownTime(float pCountdownTime)
    {
        countDownTime = pCountdownTime;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Unpause()
    {
        paused = false;
    }

    public bool AtZero()
    {
        if (paused) return false;
        if (time > 0) return false;
        return true;
    }
}
