
using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected event Action InteractableEvent;

    public virtual void Interact()
    {
        UIManager.Instance.ShowInteractInstructions(false);
    }

    /*   public void InvokeInteractableEvent()
       {

           InteractableEvent?.Invoke();
       }

       public void addListener(Action listener)
       {
           InteractableEvent += listener;
       }*/


}
