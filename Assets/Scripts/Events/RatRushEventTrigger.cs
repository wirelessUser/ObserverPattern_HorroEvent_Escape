using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatRushEventTrigger : EventManager
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            OnRatRush?.Invoke();
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare2, false);
            triggerCollider.enabled = false;
        }
    }
}
