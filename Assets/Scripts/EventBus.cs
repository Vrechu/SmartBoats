using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Event { }

public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;

    public static void Subscribe(Action<T> method)
    {
        OnEvent += method;
    }

    public static void UnSubscribe(Action<T> method)
    {
        OnEvent -= method;
    }

    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}


public class BoatReproductionEvent : Event{
    AgentData giver;
    AgentData receiver;
    public BoatReproductionEvent(AgentData cGiver, AgentData cReceiver) 
    { giver = cGiver; receiver = cReceiver; }
}

public class PirateReproductionEvent : Event
{
    AgentData giver;
    AgentData receiver;
    public PirateReproductionEvent(AgentData cGiver, AgentData cReceiver)
    { giver = cGiver; receiver = cReceiver; }
}

