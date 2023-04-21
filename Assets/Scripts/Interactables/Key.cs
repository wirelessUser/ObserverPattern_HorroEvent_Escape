using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public override void Interact()
    {
        OnKeyPickedUp?.Invoke();
        SoundManager.OnPlaySoundEffects?.Invoke(SoundType.KeyPickUp, false);
        UIManager.OnPlayerNotNearInteractable?.Invoke();
        gameObject.SetActive(false);
    }
}
