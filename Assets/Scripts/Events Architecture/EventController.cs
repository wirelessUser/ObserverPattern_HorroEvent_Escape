using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController<T>
{
    protected event Action<T> baseEvent;
    public void InvokeEvent(T type)
    {
        baseEvent?.Invoke(type);
    }

    public void AddListener(Action<T> listener)
    {
        baseEvent += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        baseEvent -= listener;
    }
}

public class GameEventController
{
    protected event Action baseEvent;
    public void InvokeEvent()
    {
        baseEvent?.Invoke();
    }

    public void AddListener(Action listener)
    {
        baseEvent += listener;
    }

    public void RemoveListener(Action listener)
    {
        baseEvent -= listener;
    }
}
