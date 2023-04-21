using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscapedEventTrigger : EventTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare3, false);
            OnPlayerEscaped?.Invoke();
        }
    }
}
