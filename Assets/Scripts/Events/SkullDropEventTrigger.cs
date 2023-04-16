using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDropEventTrigger : MonoBehaviour
{
    [SerializeField] private int m_RequiredKeysForEvent;
    private bool m_EventOccured;
    public static Action OnSkullDrop;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && !m_EventOccured && PlayerController.KeysEquipped >= m_RequiredKeysForEvent)
        {
            OnSkullDrop?.Invoke();
            m_EventOccured = true;
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare1, false);
        }
    }
}
