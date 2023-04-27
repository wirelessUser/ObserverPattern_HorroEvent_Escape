using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Normal Mono -> Call it PlayerEscapedEvent
public class PlayerEscapedEventTrigger : CoreEventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            SoundManager.Instance.PlaySoundEffects(SoundType.JumpScare3);//hardconding
            EventManager.Instance.InvokeOnPlayerEscaped();
        }
    }
}
