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
        EventService.Instance.OnPotionDrinkEvent.AddListener(onDrankPotion);
        EventService.Instance.OnRatRushEvent.AddListener(onSupernaturalEvent);
        EventService.Instance.OnSkullDropEvent.AddListener(onSupernaturalEvent);
    }

    private void OnDisable()
    {
        EventService.Instance.OnPotionDrinkEvent.RemoveListener(onDrankPotion);
        EventService.Instance.OnRatRushEvent.RemoveListener(onSupernaturalEvent);
        EventService.Instance.OnSkullDropEvent.RemoveListener(onSupernaturalEvent);
    }

    void Update()
    {
        if (playerController.PlayerState == PlayerState.Dead)
            return;

        float sanityDrop = updateSanity();

        decreaseSanity(sanityDrop);
    }

    private float updateSanity()
    {
        float sanityDrop = sanityDropRate * Time.deltaTime;
        if (playerController.PlayerState == PlayerState.InDark)
        {
            sanityDrop *= 10f;
        }
        return sanityDrop;
    }

    private void decreaseSanity(float amountToDecrease)
    {
        Mathf.Floor(sanityLevel -= amountToDecrease);
        if (sanityLevel <= 0)
        {
            sanityLevel = 0;
            GameService.Instance.GameOver();
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }

    private void increaseSanity(float amountToIncrease)
    {
        Mathf.Floor(sanityLevel += amountToIncrease);
        if (sanityLevel > 100)
        {
            sanityLevel = 100;
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }
    private void onSupernaturalEvent()
    {
        decreaseSanity(sanityDropAmountPerEvent);
    }

    private void onDrankPotion(int potionEffect)
    {
        increaseSanity(potionEffect);
    }
}