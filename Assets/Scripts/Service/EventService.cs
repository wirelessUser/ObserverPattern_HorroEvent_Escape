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

    public GameEventController OnLightSwitchToggled { get; private set; }
    public GameEventController<int> OnKeyPickedUp { get; private set; }
    public GameEventController LightsOffByGhostEvent { get; private set; }

    public GameEventController PlayerEscapedEvent { get; private set; }
    public GameEventController PlayerDeathEvent { get; private set; }

    public EventService()
    {
        OnLightSwitchToggled = new GameEventController();
        OnKeyPickedUp = new GameEventController<int>();
        LightsOffByGhostEvent = new GameEventController();

        PlayerEscapedEvent = new GameEventController();
        PlayerDeathEvent = new GameEventController();
    }
}
