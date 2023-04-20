using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOffEventTrigger : EventTrigger
{
    //  [SerializeField] private int m_KeysRequiredToTrigger;
    //  private Collider triggerCollider;
    //  public static Action OnLightsOff;


    /*   private void Awake()
      {
          triggerCollider = GetComponent<Collider>();
          triggerCollider.isTrigger = true; // We can set this as trigger in prefab itself
      }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.KeysEquipped == m_KeysRequiredToTrigger)
        {
            OnLightsOff?.Invoke();
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.SpookyGiggle, false);
            triggerCollider.enabled = false;

        }
    }
}