using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Normal Mono -> Call it SkullDrop
// All the RatRush Logic written in SkullDrop should be moved here
public class SkullDropEventTrigger : CoreEventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.Instance.KeysEquipped >= keysRequiredToTrigger)
        {
            EventManager.Instance.InvokeOnSkullDrop();
            SoundManager.Instance.PlaySoundEffects(SoundType.JumpScare1, false);
            triggerCollider.enabled = false;
        }
    }
}
