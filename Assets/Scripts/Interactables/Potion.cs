using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Interactable
{
    public override void Interact()
    {
        base.Interact();
        OnPotionDrink?.Invoke();
        SoundManager.OnPlaySoundEffects?.Invoke(SoundType.DrinkPotion, false);
        gameObject.SetActive(false);
    }
}
