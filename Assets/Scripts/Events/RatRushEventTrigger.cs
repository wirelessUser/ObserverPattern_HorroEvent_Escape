using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatRushEventTrigger : MonoBehaviour
{
    private bool m_EventOccured;
    public static Action OnRatRush;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && !m_EventOccured)
        {
            OnRatRush?.Invoke();
            m_EventOccured = true;
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare2, false);
        }
    }
}
