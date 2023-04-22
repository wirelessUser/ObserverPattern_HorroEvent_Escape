using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Responsible for handling Sanity of our Player
/// </summary>
public class
    PlayerSanity : MonoBehaviour
{
    [SerializeField] private float sanityLevel = 100.0f;
    [SerializeField] private float sanityDropRate = 0.2f;
    [SerializeField] private float sanityDropAmountPerEvent = 10f;
    [SerializeField] private UIManager UIManager;
    private float maxSanity;
    private bool isPlayerInDark = false;
    public static Action OnPlayerDeath;

    private void Start()
    {
        maxSanity = sanityLevel;
    }

    private void OnEnable()
    {
        LightsOffEventTrigger.OnLightsOffByGhost += OnLightsOffByGhost;
        LightSwitch.OnLightsSwitchToggled += OnLightsToggled;
        RatRushEventTrigger.OnRatRush += OnSupernaturalEvent;
        SkullDropEventTrigger.OnSkullDrop += OnSupernaturalEvent;
        Potion.OnPotionDrink += OnDrankPotion;
    }

    private void OnDisable()
    {
        LightsOffEventTrigger.OnLightsOffByGhost -= OnLightsOffByGhost;
        LightSwitch.OnLightsSwitchToggled -= OnLightsToggled;
        RatRushEventTrigger.OnRatRush -= OnSupernaturalEvent;
        SkullDropEventTrigger.OnSkullDrop -= OnSupernaturalEvent;
        Potion.OnPotionDrink -= OnDrankPotion;
    }

    void Update()
    {
        if (isPlayerInDark)
            DecreaseSanity(sanityDropRate * Time.deltaTime * 10);
        else
            DecreaseSanity(sanityDropRate * Time.deltaTime);

        // Hotkeys:
        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare1, false);
            OnPlayerDeath?.Invoke();
        }
    }

    public void DecreaseSanity(float amountToDecrease)
    {
        sanityLevel -= amountToDecrease;
        if (sanityLevel <= 0)
        {
            sanityLevel = 0;
            GameOver();
        }
        UIManager.UpdateInsanity(1f - sanityLevel / maxSanity);
    }

    public void IncreaseSanity(float amountToIncrease)
    {
        sanityLevel += amountToIncrease;
        if (sanityLevel > 100)
            sanityLevel = 100;
        UIManager.UpdateInsanity(1f - sanityLevel / maxSanity);
    }


    void GameOver()
    {
        OnPlayerDeath?.Invoke();
    }

    private void OnLightsOffByGhost()
    {
        isPlayerInDark = true;
    }

    private void OnLightsToggled(bool lightsState)
    {
        isPlayerInDark = !lightsState;
    }

    private void OnSupernaturalEvent()
    {
        DecreaseSanity(sanityDropAmountPerEvent);
    }

    private void OnDrankPotion()
    {
        IncreaseSanity(20);
    }
}