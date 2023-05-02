using UnityEngine;

public class KeyView : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        int currentKeys = GameService.Instance.GetPlayerController().KeysEquipped;
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.KeyPickUp);
        EventService.Instance.KeyPickedUpEvent.InvokeEvent(++currentKeys);
        gameObject.SetActive(false);
    }
}
