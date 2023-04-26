
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        UIManager.Instance.ShowInteractInstructions(false);
    }
}
