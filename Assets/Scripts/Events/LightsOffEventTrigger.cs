using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOffEventTrigger : CoreEventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.KeysEquipped == keysRequiredToTrigger)
        {
            EventManager.instance.InvokeOnLightsOffByGhost();
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.SpookyGiggle, false);
            triggerCollider.enabled = false;

        }
    }
}