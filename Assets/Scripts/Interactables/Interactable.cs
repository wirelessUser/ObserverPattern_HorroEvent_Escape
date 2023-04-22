
using UnityEngine;

public class Interactable : EventTrigger
{
    public virtual void Interact()
    {
        UIManager.OnPlayerNotNearInteractable?.Invoke();
    }
}
