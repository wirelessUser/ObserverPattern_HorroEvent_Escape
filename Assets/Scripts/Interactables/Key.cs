using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    private static int keysEquipped = 0;
    public override void Interact()
    {
        Debug.Log("Key Picked Up");
        base.Interact();
        keysEquipped++;

        //  EventManager.Instance.InvokeOnKeyPickedUp(keysEquipped);
        // InvokeInteractableEvent();
        SoundManager.Instance.PlaySoundEffects(SoundType.KeyPickUp, false);
        gameObject.SetActive(false);
    }
}
