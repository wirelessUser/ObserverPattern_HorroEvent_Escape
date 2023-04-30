using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, I_Interactable
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
        UIManager.Instance.ShowInteractInstructions(false);
        DoorInteraction();
    }
    private void DoorInteraction()
    {
        switch (currentState)
        {
            case DoorState.Locked:
                if (ServiceLocator.Instance.GetPlayerController().GetKeys() >= keysRequiredToOpen)
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
