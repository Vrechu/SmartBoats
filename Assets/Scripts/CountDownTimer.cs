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

    /// <summary>
    /// </summary>
    /// <param name="cMaxCountDownTime">The starting max value of the timer.</param>
    /// <param name="cStartingTime">The time the timer starts on.</param>
    /// <param name="cResetOnZero">When true, the timer resets to max value on 0.</param>
    /// <param name="cPaused">When true, the timer does not automatically start counting down until manually unpaused.</param>
    /// <param name="cClampMax">When true, the timer cannot go over the max value when time is added.</param>
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
        CheckZeroTime();
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

    /// <summary>
    /// </summary>
    /// <param name="pThreshold"></param>
    /// <returns>Returns true when the time is below the given threshold.</returns>
    public bool BelowThreshold(float pThreshold)
    {
        if (time < pThreshold) return true;
        return false;
    }

    /// <summary>
    /// Adds time to the timer.
    /// If the timer is clamped the time will not go above the max time.
    /// </summary>
    /// <param name="pTime">Time to add in seconds.</param>
    public void AddTime(float pTime)
    {
        time += pTime;
        if (clampMaxValue && time > countDownTime) time = countDownTime;
    }

    public void Clamp(bool pClamp)
    {
        clampMaxValue = pClamp;
    }

    private void CheckZeroTime()
    {
        if (countDownTime <= 0)
        {
            Debug.LogError("Countdown time = 0!");
            paused = true;
        }
    }
}
