using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Take Reference of LightSwitch.cs Interactable , and make each interactable implemnt the I_Interactable
public class Door : Interactable
{
    [SerializeField] private float swingAngle;
    [SerializeField] private int keysRequiredToOpen;
    private DoorState currentState;


    private void Start()
    {
        currentState = DoorState.Locked;
    }
    public override void Interact()
    {
        base.Interact();
        DoorInteraction();
    }
    private void DoorInteraction()
    {
        switch (currentState)
        {
            case DoorState.Locked:
                if (PlayerController.Instance.KeysEquipped >= keysRequiredToOpen)
                {
                    transform.Rotate(0f, transform.rotation.y + swingAngle, 0f);
                    currentState = DoorState.Open;
                    SoundManager.Instance.PlaySoundEffects(SoundType.DoorOpen, false);
                }
                break;
            case DoorState.Close:
                transform.Rotate(0f, transform.rotation.y + swingAngle, 0f);
                currentState = DoorState.Open;
                SoundManager.Instance.PlaySoundEffects(SoundType.DoorOpen, false);
                break;
            case DoorState.Open:
                transform.Rotate(0f, transform.rotation.y - swingAngle, 0f);
                currentState = DoorState.Close;
                SoundManager.Instance.PlaySoundEffects(SoundType.DoorSlam, false);
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
