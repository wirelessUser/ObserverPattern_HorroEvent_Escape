using UnityEngine;
using System.Collections;

/// <summary>
/// Responsible for handling Sanity of our Player
/// </summary>
public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float m_SanityLevel = 100.0f;
    [SerializeField] private float m_SanityDropRate = 0.5f;
    [SerializeField] private UIManager m_UIManager;
    private float m_MaxSanity;

    private bool m_IsPlayerInDark = false;

    private void Start()
    {
        m_MaxSanity = m_SanityLevel;
    }

    public void DecreaseSanity(float amountToDecrease)
    {
        m_SanityLevel -= amountToDecrease;
        if (m_SanityLevel <= 0)
        {
            m_SanityLevel = 0;
            GameOver();
        }
    }

    public void IncreaseSanity(float amountToIncrease)
    {
        m_SanityLevel += amountToIncrease;
        if (m_SanityLevel > 100)
            m_SanityLevel = 100;
    }

    void Update()
    {
        if(m_IsPlayerInDark)
            DecreaseSanity(m_SanityDropRate);
        m_UIManager.UpdateInsanity(1f - m_SanityLevel/m_MaxSanity);

        // Hotkeys:
        if(Input.GetKeyDown(KeyCode.P))
        {
            DecreaseSanity(10f);
        }
    }

    void GameOver()
    {
        // Implement game over logic here
    }
}