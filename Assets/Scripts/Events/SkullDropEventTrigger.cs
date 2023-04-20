using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDropEventTrigger : EventTrigger
{
    /* [SerializeField] private int m_RequiredKeysForEvent;
     private Collider triggerCollider;
     public static Action OnSkullDrop;

     private void Awake()
     {
         triggerCollider = GetComponent<Collider>();
         GetComponent<Collider>().isTrigger = true;
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.KeysEquipped >= m_KeysRequiredToTrigger)
        {
            OnSkullDrop?.Invoke();
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.JumpScare1, false);
            triggerCollider.enabled = false;
        }
    }
}
