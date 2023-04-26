using System;
using UnityEngine;

public class GameEvent : EventController
{

    public override void addListener(Action listener)
    {
        baseEvent += listener;
    }
    public override void removeListener(Action listener)
    {
        baseEvent -= listener;
    }
}
