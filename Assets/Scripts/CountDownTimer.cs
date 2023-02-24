using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct CountdownTimer
{
    public float countDownTime { get; private set; }
    public float time { get; private set; }
    private bool resetOnZero;
    public bool paused { get; private set; }
    public bool clampMaxValue { get; private set; }

    public CountdownTimer(float cMaxCountDownTime, float cStartingTime = -1, bool cResetOnZero = false, bool cPaused = false, bool cClampMax = true) 
    {
        countDownTime = cMaxCountDownTime;
        if (cStartingTime > 0) time = cStartingTime;
        else time = countDownTime;

        resetOnZero = cResetOnZero;
        paused = cPaused;
        clampMaxValue = cClampMax;
    }

    /// <summary>
    /// Counts down the timer.
    /// Best placed in Update function.
    /// </summary>
    /// <returns>Returns true when the timer reaches 0.</returns>
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

    /// <summary>
    /// Sets the countdown back to it's starting time.
    /// If the timer is not paused it will continue countdown.
    /// </summary>
    public void Reset()
    {
        time = countDownTime;
    }

    /// <summary>
    /// Sets the countdown time.
    /// </summary>
    /// <param name="pCountdownTime">Time it takes for the timer to count down in seconds.</param>
    public void SetCountdownTime(float pCountdownTime)
    {
        countDownTime = pCountdownTime;
    }

    /// <summary>
    /// Pauses the countdown. Does not reset.
    /// </summary>
    public void Pause()
    {
        paused = true;
    }

    /// <summary>
    /// resumes the countdown. Does not reset.
    /// </summary>
    public void Unpause()
    {
        paused = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns true if the timer has reached zero.</returns>
    public bool AtZero()
    {
        if (paused) return false;
        if (time > 0) return false;
        return true;
    }

    public bool BelowThreshold(float pThreshold)
    {
        if (time < pThreshold) return true;
        return false;
    }

    public void AddTime(float pTime)
    {
        time += pTime;
        if (clampMaxValue && time > countDownTime) time = countDownTime;
    }

    public void Clamp(bool pClamp)
    {
        clampMaxValue = pClamp;
    }
}
