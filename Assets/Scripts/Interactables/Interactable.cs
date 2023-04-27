
using System;
using UnityEngine;

//TODO -> As there is only one line in Interact, Inheritance is Overkill, we have created I_Interactable interface
// Delete This Mono , Each Interactable will be seperate Mono, and They will implement I_Interactable 

public abstract class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        UIManager.Instance.ShowInteractInstructions(false);
    }
}
