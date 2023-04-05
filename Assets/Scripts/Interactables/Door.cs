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

    private void DoorInteraction()
    {
        switch(m_CurrentState)
        {
            case DoorState.Locked:
                if (PlayerController.s_KeysEquipped >= m_RequiredKeys)
                {
                    transform.Rotate(0f, m_SwingAngle, 0f);
                    m_CurrentState = DoorState.Open;
                }
                break;
            case DoorState.Close:
                transform.Rotate(0f, m_SwingAngle, 0f);
                m_CurrentState = DoorState.Open;
                break;
            case DoorState.Open:
                transform.Rotate(0f, -m_SwingAngle, 0f);
                m_CurrentState = DoorState.Close;
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            PlayerController.OnPlayerInteracted += DoorInteraction;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            PlayerController.OnPlayerInteracted -= DoorInteraction;
    }

    public enum DoorState
    {
        Open,
        Close,
        Locked
    }
}
