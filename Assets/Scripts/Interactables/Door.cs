using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float swingAngle;
    [SerializeField] private int keysRequiredToOpen;
    [SerializeField] private SoundService soundService;
    private DoorState currentState;


    private void Start()
    {
        currentState = DoorState.Locked;
    }
    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        DoorInteraction();
    }

    private void DoorInteraction()
    {
        switch (currentState)
        {
            case DoorState.Locked:
                if (GameService.Instance.GetPlayerController().GetKeys() >= keysRequiredToOpen)
                {
                    transform.Rotate(0f, transform.rotation.y + swingAngle, 0f);
                    currentState = DoorState.Open;
                    soundService.PlaySoundEffects(SoundType.DoorOpen);
                }
                break;
            case DoorState.Close:
                transform.Rotate(0f, transform.rotation.y + swingAngle, 0f);
                currentState = DoorState.Open;
                soundService.PlaySoundEffects(SoundType.DoorOpen);
                break;
            case DoorState.Open:
                transform.Rotate(0f, transform.rotation.y - swingAngle, 0f);
                currentState = DoorState.Close;
                soundService.PlaySoundEffects(SoundType.DoorSlam);
                break;
        }
    }

    public enum DoorState
    {
        Open,
        Close,
        Locked
    }
}
