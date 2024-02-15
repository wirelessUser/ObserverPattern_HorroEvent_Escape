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

    public EventController OnLightSwitchToggled { get; private set; }
    public EventController<int> OnKeyPickedUp { get; private set; }
    public EventController OnLightsOffByGhostEvent { get; private set; }

    public EventController PlayerEscapedEvent { get; private set; }
    public EventController PlayerDeathEvent { get; private set; }
    public EventController OnratRushEvent { get; private set; }
    public EventController OnSkullDrop { get; private set; }
    public EventController<float> OnPotionDrink { get; private set; }

    public EventService()
    {
        OnLightSwitchToggled = new EventController();
        OnKeyPickedUp = new EventController<int>();
        OnLightsOffByGhostEvent = new EventController();
        OnSkullDrop = new EventController();
        OnPotionDrink = new EventController<float>();

        OnratRushEvent = new EventController();

        PlayerEscapedEvent = new EventController();
        PlayerDeathEvent = new EventController();
    }
}
