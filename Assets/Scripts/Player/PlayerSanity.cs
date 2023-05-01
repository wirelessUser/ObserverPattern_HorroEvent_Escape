using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float sanityLevel = 100.0f;
    [SerializeField] private float sanityDropRate = 0.2f;
    [SerializeField] private float sanityDropAmountPerEvent = 10f;
    private float maxSanity;
    private PlayerController playerController;

    private void Start()
    {
        maxSanity = sanityLevel;
        playerController = GameService.Instance.GetPlayerController();
    }

    private void OnEnable()
    {
        EventService.Instance.LightSwitchToggleEvent.AddListener(OnLightsToggled);
        EventService.Instance.LightsOffByGhostEvent.AddListener(OnLightsOffByGhost);
        EventService.Instance.PotionDrinkEvent.AddListener(OnDrankPotion);
        EventService.Instance.RatRushEvent.AddListener(OnSupernaturalEvent);
        EventService.Instance.SkullDropEvent.AddListener(OnSupernaturalEvent);
    }

    private void OnDisable()
    {
        EventService.Instance.LightSwitchToggleEvent.RemoveListener(OnLightsToggled);
        EventService.Instance.LightsOffByGhostEvent.RemoveListener(OnLightsOffByGhost);
        EventService.Instance.PotionDrinkEvent.RemoveListener(OnDrankPotion);
        EventService.Instance.RatRushEvent.RemoveListener(OnSupernaturalEvent);
        EventService.Instance.SkullDropEvent.RemoveListener(OnSupernaturalEvent);
    }

    void Update()
    {
        if (playerController.PlayerState == PlayerState.Dead)
            return;

        float sanityDrop = sanityDropRate * Time.deltaTime;
        if (playerController.PlayerState == PlayerState.InDark) {
            sanityDrop *= 10f; 
        }
            
        DecreaseSanity(sanityDrop);
    }

    public void DecreaseSanity(float amountToDecrease)
    {
        Mathf.Floor(sanityLevel -= amountToDecrease);
        if (sanityLevel <= 0)
        {
            sanityLevel = 0;
            GameService.Instance.GameOver();
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }

    private void IncreaseSanity(float amountToIncrease)
    {
        Mathf.Floor(sanityLevel += amountToIncrease);
        if (sanityLevel > 100)
        {
            sanityLevel = 100;
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }

    void GameOver()
    {
        Debug.Log("Player Died");
        playerController.PlayerState = PlayerState.Dead; // playerstate is state - outside classes should not be changing state from outside!
        EventService.Instance.PlayerDeathEvent.InvokeEvent();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.JumpScare1);
    }

    private void OnLightsOffByGhost()
    {
        playerController.PlayerState = PlayerState.InDark;
    }

    private void OnLightsToggled()
    {
        if (playerController.PlayerState == PlayerState.InDark)
            playerController.PlayerState = PlayerState.None;
        else
            playerController.PlayerState = PlayerState.InDark;
    }

    private void OnSupernaturalEvent()
    {
        DecreaseSanity(sanityDropAmountPerEvent);
    }

    private void OnDrankPotion(int potionEffect)
    {
        IncreaseSanity(potionEffect);
    }
}