using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private SoundService soundService;

    public void Interact()
    {
        int currentKeys = GameService.Instance.GetPlayerController().GetKeys();
        GameService.Instance.GetInstructionView().HideInstruction();
        EventService.Instance.KeyPickedUpEvent.InvokeEvent(++currentKeys);

        soundService.PlaySoundEffects(SoundType.KeyPickUp);
        gameObject.SetActive(false);
    }
}
