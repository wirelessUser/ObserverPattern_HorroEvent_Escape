
using UnityEngine;

public class Interactable : EventManager
{
    public virtual void Interact()
    {
        UIManager.OnPlayerNotNearInteractable?.Invoke();
    }
}
