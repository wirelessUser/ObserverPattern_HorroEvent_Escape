using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Responsible for handling Sanity of our Player
/// </summary>
public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float m_SanityLevel = 100.0f;
    [SerializeField] private float m_SanityDropRate = 0.1f;
    [SerializeField] private float m_SanityDropAmountPerEvent = 10f;
    [SerializeField] private UIManager m_UIManager;
    private float m_MaxSanity;
    private bool m_IsPlayerInDark = false;
    
    public static Action OnPlayerDeath;

    private void Start()
    {
        m_MaxSanity = m_SanityLevel;
    }

    private void OnEnable()
    {
        LightsOffEventTrigger.OnLightsOff += OnLightsOff;
        LightSwitch.OnLightsOn += OnLightsOn;
        RatRushEventTrigger.OnRatRush += OnSupernaturalEvent;
        SkullDropEventTrigger.OnSkullDrop += OnSupernaturalEvent;
    }

    private void OnDisable()
    {
        LightsOffEventTrigger.OnLightsOff -= OnLightsOff;
        LightSwitch.OnLightsOn -= OnLightsOn;
        RatRushEventTrigger.OnRatRush -= OnSupernaturalEvent;
        SkullDropEventTrigger.OnSkullDrop -= OnSupernaturalEvent;
    }

    void Update()
    {
        if (m_IsPlayerInDark)
            DecreaseSanity(m_SanityDropRate * Time.deltaTime * 10);
        else
            DecreaseSanity(m_SanityDropRate * Time.deltaTime);

        // Hotkeys:
        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare1, false);
            OnPlayerDeath?.Invoke();
        }
    }

    public void DecreaseSanity(float amountToDecrease)
    {
        m_SanityLevel -= amountToDecrease;
        if (m_SanityLevel <= 0)
        {
            m_SanityLevel = 0;
            GameOver();
        }
        m_UIManager.UpdateInsanity(1f - m_SanityLevel / m_MaxSanity);
    }

    public void IncreaseSanity(float amountToIncrease)
    {
        m_SanityLevel += amountToIncrease;
        if (m_SanityLevel > 100)
            m_SanityLevel = 100;
    }


    void GameOver()
    {
        // Implement game over logic here
        SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare1, false);
        OnPlayerDeath?.Invoke();
    }

    private void OnLightsOff()
    {
        m_IsPlayerInDark = true;
    }

    private void OnLightsOn()
    {
        m_IsPlayerInDark = false;
    }

    private void OnSupernaturalEvent()
    {
        DecreaseSanity(m_SanityDropAmountPerEvent);
    }
}