using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private float m_SwingAngle;
    [SerializeField]
    private int m_RequiredKeys;

    private DoorState m_CurrentState;

    private void Start()
    {
        m_CurrentState = DoorState.Locked;
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
        switch (m_CurrentState)
        {
            case DoorState.Locked:
                if (PlayerController.KeysEquipped >= m_RequiredKeys)
                {
                    transform.Rotate(0f, transform.rotation.y + m_SwingAngle, 0f);
                    m_CurrentState = DoorState.Open;
                    SoundManager.OnPlaySoundEffects?.Invoke(SoundType.DoorOpen, false);
                }
                break;
            case DoorState.Close:
                transform.Rotate(0f, transform.rotation.y + m_SwingAngle, 0f);
                m_CurrentState = DoorState.Open;
                SoundManager.OnPlaySoundEffects?.Invoke(SoundType.DoorOpen, false);
                break;
            case DoorState.Open:
                transform.Rotate(0f, transform.rotation.y - m_SwingAngle, 0f);
                m_CurrentState = DoorState.Close;
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
