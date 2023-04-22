using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscapedEventTrigger : CoreEventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            SoundManager.Instance.PlaySoundEffects(SoundType.JumpScare3, false);
            EventManager.Instance.InvokeOnPlayerEscaped();
        }
    }
}
