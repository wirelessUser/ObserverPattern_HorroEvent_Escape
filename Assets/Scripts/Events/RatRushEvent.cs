using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Normal Mono -> Call it RatRushEvent
// All the RatRush Logic written in RatRush should be moved here
public class RatRushEvent : MonoBehaviour
{
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            EventService.Instance.RatRushEvent.InvokeEvent();
            SoundManager.Instance.PlaySoundEffects(soundToPlay, false);
            GetComponent<Collider>().enabled = false;
        }
    }
}
