using System;

public abstract class EventController
{
    protected event Action baseEvent;


    public void InvokeEvent()
    {
        baseEvent?.Invoke();
    }

    public abstract void addListener(Action listener);

    public abstract void removeListener(Action listener);

}
