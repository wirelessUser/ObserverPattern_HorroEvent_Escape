using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOffEventTrigger : CoreEventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.Instance.KeysEquipped == keysRequiredToTrigger)
        {
            EventManager.Instance.InvokeOnLightsOffByGhost();
            SoundManager.Instance.PlaySoundEffects(SoundType.SpookyGiggle, false);
            triggerCollider.enabled = false;

        }
    }
}