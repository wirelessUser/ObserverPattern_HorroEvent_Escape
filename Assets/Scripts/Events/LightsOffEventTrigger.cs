using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOffEventTrigger : EventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.KeysEquipped == keysRequiredToTrigger)
        {
            OnLightsOff?.Invoke();
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.SpookyGiggle, false);
            triggerCollider.enabled = false;

        }
    }
}