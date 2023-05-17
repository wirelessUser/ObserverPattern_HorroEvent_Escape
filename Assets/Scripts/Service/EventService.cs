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

    public EventController OnLightSwitchToggleEvent { get; private set; }

    public EventController<int> OnKeyPickedUpEvent { get; private set; }

    public EventController<int> OnPotionDrinkEvent { get; private set; }

    public EventController OnLightsOffByGhostEvent { get; private set; }

    public EventController OnRatRushEvent { get; private set; }

    public EventController OnSkullDropEvent { get; private set; }

    public EventController OnPlayerEscapedEvent { get; private set; }

    public EventController OnPlayerDeathEvent { get; private set; }

    public EventService()
    {
        OnLightSwitchToggleEvent = new EventController();
        OnKeyPickedUpEvent = new EventController<int>();
        OnPotionDrinkEvent = new EventController<int>();
        OnLightsOffByGhostEvent = new EventController();
        OnRatRushEvent = new EventController();
        OnSkullDropEvent = new EventController();
        OnPlayerEscapedEvent = new EventController();
        OnPlayerDeathEvent = new EventController();
    }
}
