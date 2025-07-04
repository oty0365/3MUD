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

public class GameEventManager : HalfSingleMono<GameEventManager>
{

    public Dictionary<InGameEvent,Action> eventsDict = new Dictionary<InGameEvent,Action>();


    void Start()
    {
    }

    public void UploadEvent(InGameEvent inGameEvent,Action action)
    {
        eventsDict.Add(inGameEvent, action);
    }
  
}
