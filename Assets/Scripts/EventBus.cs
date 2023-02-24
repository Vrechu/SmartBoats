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
    public AgentData parent1;
    public AgentData parent2;
    public BoatReproductionEvent(AgentData cParen1, AgentData cParen2) 
    { parent1 = cParen1; parent2 = cParen2; }
}

public class PirateReproductionEvent : Event
{
    public AgentData parent1;
    public AgentData parent2;
    public PirateReproductionEvent(AgentData cParent1, AgentData cParent2)
    { parent1 = cParent1; parent2 = cParent2; }
}

public class AgentDiedEvent : Event
{
    public uint index;
    public AgentDiedEvent(uint cIndex) { index = cIndex; }
}

