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

    public GameEventController PotionDrinkEvent { get; private set; }

    public GameEventController LightsOffByGhostEvent { get; private set; }

    public GameEventController RatRushEvent { get; private set; }

    public GameEventController SkullDropEvent { get; private set; }

    public GameEventController PlayerEscapedEvent { get; private set; }

    public GameEventController PlayerDeathEvent { get; private set; }

    public EventService()
    {
        this.LightSwitchToggleEvent = new GameEventController();
        this.KeyPickedUpEvent = new GameEventController<int>();
        this.PotionDrinkEvent = new GameEventController();
        this.LightsOffByGhostEvent = new GameEventController();
        this.RatRushEvent = new GameEventController();
        this.SkullDropEvent = new GameEventController();
        this.PlayerEscapedEvent = new GameEventController();
        this.PlayerDeathEvent = new GameEventController();
    }
}
