using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum InGameEvent
{
    None,
    WeaponSelection,
    AugmentOrItemSelection,
    FullHp,
    HpUp,
    AtkUp,
    DefUp,
    SpdUp,
    WipUp
}

public interface IEvent
{
    void UploadEvent(InGameEvent inGameEvent, Action action);
    void RemoveEvent(InGameEvent inGameEvent);
}

public class GameEventManager : HalfSingleMono<GameEventManager>
{

    public Dictionary<InGameEvent,Action> eventsDict = new Dictionary<InGameEvent,Action>();
    
    public void UploadEvent(InGameEvent evt, Action action)
    {
        if (eventsDict.ContainsKey(evt))
            eventsDict[evt] += action;
        else
            eventsDict[evt] = action;
    }

    public void RemoveEvent(InGameEvent inGameEvent)
    {
        eventsDict.Remove(inGameEvent);
    }
  
}
