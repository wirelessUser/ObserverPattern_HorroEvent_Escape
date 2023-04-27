using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Normal Mono -> Call it LightsOffByGhotEvent
public class LightsOffEventTrigger : CoreEventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.Instance.KeysEquipped == keysRequiredToTrigger)
        {
            EventManager.Instance.InvokeOnLightsOffByGhost();
            // Expose SOundType Enum ference and set it from inspector, dont hardcode
            SoundManager.Instance.PlaySoundEffects(SoundType.SpookyGiggle, false);
            triggerCollider.enabled = false;

        }
    }
}