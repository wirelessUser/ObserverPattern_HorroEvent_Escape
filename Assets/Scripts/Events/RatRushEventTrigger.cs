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
            EventManager.Instance.InvokeOnRatRush();
            SoundManager.Instance.PlaySoundEffects(SoundType.JumpScare2, false);
            triggerCollider.enabled = false;
        }
    }
}
