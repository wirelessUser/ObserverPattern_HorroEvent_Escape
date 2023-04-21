using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private float swingAngle;
    [SerializeField]
    private int requiredKeys;

    private DoorState currentState;

    private void Start()
    {
        currentState = DoorState.Locked;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerInteractionHandler.OnPlayerInteracted += DoorInteraction;
            UIManager.OnPlayerNearInteractable?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerInteractionHandler.OnPlayerInteracted -= DoorInteraction;
            UIManager.OnPlayerNotNearInteractable?.Invoke();
        }
    }

    private void DoorInteraction()
    {
        switch (currentState)
        {
            case DoorState.Locked:
                if (PlayerController.KeysEquipped >= requiredKeys)
                {
                    transform.Rotate(0f, transform.rotation.y + swingAngle, 0f);
                    currentState = DoorState.Open;
                    SoundManager.OnPlaySoundEffects?.Invoke(SoundType.DoorOpen, false);
                }
                break;
            case DoorState.Close:
                transform.Rotate(0f, transform.rotation.y + swingAngle, 0f);
                currentState = DoorState.Open;
                SoundManager.OnPlaySoundEffects?.Invoke(SoundType.DoorOpen, false);
                break;
            case DoorState.Open:
                transform.Rotate(0f, transform.rotation.y - swingAngle, 0f);
                currentState = DoorState.Close;
                SoundManager.OnPlaySoundEffects?.Invoke(SoundType.DoorSlam, false);
                break;
        }
    }

    private void OnDestroy()
    {
        PlayerInteractionHandler.OnPlayerInteracted -= DoorInteraction;
    }

    public enum DoorState
    {
        Open,
        Close,
        Locked
    }
}
