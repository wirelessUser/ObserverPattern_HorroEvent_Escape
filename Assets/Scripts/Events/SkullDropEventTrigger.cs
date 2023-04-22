using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDropEventTrigger : EventManager
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.KeysEquipped >= keysRequiredToTrigger)
        {
            OnSkullDrop?.Invoke();
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare1, false);
            triggerCollider.enabled = false;
        }
    }
}
