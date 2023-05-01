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
        LightSwitchToggleEvent = new GameEventController();
        KeyPickedUpEvent = new GameEventController<int>();
        PotionDrinkEvent = new GameEventController();
        LightsOffByGhostEvent = new GameEventController();
        RatRushEvent = new GameEventController();
        SkullDropEvent = new GameEventController();
        PlayerEscapedEvent = new GameEventController();
        PlayerDeathEvent = new GameEventController();
    }
}
