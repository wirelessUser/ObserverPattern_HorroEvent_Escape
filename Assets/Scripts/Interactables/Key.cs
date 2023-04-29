using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, I_Interactable
{
    //TODO -> Key Counter SHould be in Player Scriptable Object
    private static int keysEquipped = 0;

    public void Interact()
    {
        Debug.Log("Key Picked Up");
        UIManager.Instance.ShowInteractInstructions(false);
        
        keysEquipped++;
        EventService.Instance.KeyPickedUpEvent.InvokeEvent(keysEquipped);
        SoundManager.Instance.PlaySoundEffects(SoundType.KeyPickUp, false);
        gameObject.SetActive(false);
    }
}
