using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Normal Mono -> Call it RatRushEvent
// All the RatRush Logic written in RatRush should be moved here
public class RatRushEventTrigger : CoreEventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            EventManager.Instance.InvokeOnRatRush();
            SoundManager.Instance.PlaySoundEffects(SoundType.JumpScare2, false);
            triggerCollider.enabled = false;
        }
    }
}
