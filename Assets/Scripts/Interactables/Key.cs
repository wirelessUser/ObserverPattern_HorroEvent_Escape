using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static Action OnKeyPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerInteractedEventTrigger.OnPlayerInteracted += InteractedWithKey;
            UIManager.OnPlayerNearInteractable?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerInteractedEventTrigger.OnPlayerInteracted -= InteractedWithKey;
            UIManager.OnPlayerNotNearInteractable?.Invoke();
        }
    }

    private void InteractedWithKey()
    {
        OnKeyPickedUp?.Invoke();
        PlayerInteractedEventTrigger.OnPlayerInteracted -= InteractedWithKey;
        SoundManager.OnPlaySoundEffects?.Invoke(SoundType.KeyPickUp, false);
        UIManager.OnPlayerNotNearInteractable?.Invoke();
        gameObject.SetActive(false);
    }
}
