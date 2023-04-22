using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatRushEventTrigger : CoreEventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            EventManager.instance.InvokeOnRatRush();
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare2, false);
            triggerCollider.enabled = false;
        }
    }
}
