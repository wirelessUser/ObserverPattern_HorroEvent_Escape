
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected int keysRequiredToTrigger;

    public virtual void Interact()
    {
        UIManager.instance.ShowInteractInstructions(false);
    }
}
