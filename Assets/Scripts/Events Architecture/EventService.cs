using System;
using System.Diagnostics;
public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }
    // Todo List All Events Here
    public GameEventController LightSwitchToggleEvent { get; private set; }

    public GameEventController<int> KeyPickedUpEvent { get; private set; }

    public EventService()
    {
        this.LightSwitchToggleEvent = new GameEventController();
        this.KeyPickedUpEvent = new GameEventController<int>();
    }
}
