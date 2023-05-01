using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        int currentKeys = GameService.Instance.GetPlayerController().GetKeys();
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.KeyPickUp);
        EventService.Instance.KeyPickedUpEvent.InvokeEvent(++currentKeys);
        gameObject.SetActive(false);
    }
}
