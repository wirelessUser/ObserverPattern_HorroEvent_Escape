using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Normal Mono -> Call it SkullDrop
// All the RatRush Logic written in SkullDrop should be moved here
public class SkullDropEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.Instance.KeysEquipped >= keysRequiredToTrigger)
        {
            EventService.Instance.SkullDropEvent.InvokeEvent();
            SoundManager.Instance.PlaySoundEffects(soundToPlay, false);
            GetComponent<Collider>().enabled = false;
        }
    }
}
