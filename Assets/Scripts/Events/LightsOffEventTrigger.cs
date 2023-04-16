using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOffEventTrigger : MonoBehaviour
{   
    [SerializeField] int m_KeysRequiredToTrigger;
    public static Action OnLightsOff;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.KeysEquipped == m_KeysRequiredToTrigger)
        {
            OnLightsOff?.Invoke();
            SoundManager.OnPlaySoundEffects?.Invoke(SoundType.SpookyGiggle, false);
        }
    }
}