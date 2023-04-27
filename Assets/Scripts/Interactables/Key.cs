using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Take Reference of LightSwitch.cs Interactable , and make each interactable implemnt the I_Interactable
public class Key : Interactable
{
    //TODO -> Key Counter SHould be in Player Scriptable Object
    private static int keysEquipped = 0;

    public override void Interact()
    {
        Debug.Log("Key Picked Up");
        base.Interact();
        keysEquipped++;


        EventService.Instance.KeyPickedUpEvent.InvokeEvent(keysEquipped);
        SoundManager.Instance.PlaySoundEffects(SoundType.KeyPickUp, false);
        gameObject.SetActive(false);
    }
}
