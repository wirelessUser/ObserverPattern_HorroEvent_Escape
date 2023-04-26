using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
