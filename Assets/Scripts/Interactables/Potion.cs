using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, I_Interactable
{
    public void Interact()
    {
        UIManager.Instance.ShowInteractInstructions(false);
        EventManager.Instance.InvokeOnPotionDrink();
        SoundManager.Instance.PlaySoundEffects(SoundType.DrinkPotion, false);
        gameObject.SetActive(false);
    }
}
