using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public static Action OnDrankPotion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerInteractedEventTrigger.OnPlayerInteracted += InteractedWithPotion;
            UIManager.OnPlayerNearInteractable?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerInteractedEventTrigger.OnPlayerInteracted -= InteractedWithPotion;
            UIManager.OnPlayerNotNearInteractable?.Invoke();
        }
    }

    private void InteractedWithPotion()
    {
        OnDrankPotion?.Invoke();
        PlayerInteractedEventTrigger.OnPlayerInteracted -= InteractedWithPotion;
        SoundManager.OnPlaySoundEffects?.Invoke(SoundType.DrinkPotion, false);
        UIManager.OnPlayerNotNearInteractable?.Invoke();
        gameObject.SetActive(false);
    }
}
