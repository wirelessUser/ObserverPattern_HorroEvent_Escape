using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Take Reference of LightSwitch.cs Interactable , and make each interactable implemnt the I_Interactable
public class Potion : Interactable
{
    public override void Interact()
    {
        base.Interact();
        EventManager.Instance.InvokeOnPotionDrink();
        SoundManager.Instance.PlaySoundEffects(SoundType.DrinkPotion, false);
        gameObject.SetActive(false);
    }
}
