using UnityEngine;
using System.Collections;
using System;


public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float sanityLevel = 100.0f;
    [SerializeField] private float sanityDropRate = 0.2f;
    [SerializeField] private float sanityDropAmountPerEvent = 10f;
    [SerializeField] private UIManager UIManager;
    private float maxSanity;
    private bool isPlayerInDark = true;
    private bool isAlive = true;

    private void Start()
    {
        maxSanity = sanityLevel;
    }

    private void OnEnable()
    {
        // TODO -> Make Every Event in EventService , Remove EventManager

        EventService.Instance.LightSwitchToggleEvent.AddListener(OnLightsToggled);
        EventManager.Instance.OnLightsOffByGhost += OnLightsOffByGhost;
        EventManager.OnRatRush += OnSupernaturalEvent;
        EventManager.OnSkullDrop += OnSupernaturalEvent;
        EventManager.OnPotionDrink += OnDrankPotion;
    }

    private void OnDisable()
    {
        EventService.Instance.LightSwitchToggleEvent.RemoveListener(OnLightsToggled);

        EventManager.Instance.OnLightsOffByGhost -= OnLightsOffByGhost;
        EventManager.OnRatRush -= OnSupernaturalEvent;
        EventManager.OnSkullDrop -= OnSupernaturalEvent;
        EventManager.OnPotionDrink -= OnDrankPotion;
    }

    void Update()
    {
        if (!isAlive)
            return;

        if (isPlayerInDark)
            DecreaseSanity(sanityDropRate * Time.deltaTime * 10);
        else
            DecreaseSanity(sanityDropRate * Time.deltaTime);
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
        {
            sanityLevel = 100;
        }
        UIManager.UpdateInsanity(1f - sanityLevel / maxSanity);
    }


    void GameOver()
    {
        Debug.Log("Player Died");
        isAlive = false;
        EventManager.Instance.InvokeOnPlayerDeath();
        SoundManager.Instance.PlaySoundEffects(SoundType.JumpScare1, false);
    }

    private void OnLightsOffByGhost()
    {
        isPlayerInDark = true;
    }

    private void OnLightsToggled()
    {
        Debug.Log("PlayerSanity - OnLightsToggled");
        isPlayerInDark = !isPlayerInDark;
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