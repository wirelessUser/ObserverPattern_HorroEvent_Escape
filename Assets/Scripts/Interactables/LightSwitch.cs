using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private List<Light> m_Lightsources = new List<Light>();
    private SwitchState m_CurrentState;

    public static Action OnLightsOn;

    private void Start()
    {
        m_CurrentState = SwitchState.On;
        LightsOffEventTrigger.OnLightsOff += OnLigthsOff;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        LightsOffEventTrigger.OnLightsOff -= OnLigthsOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            PlayerInteractionHandler.OnPlayerInteracted += OnInteractedWithSwitch;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            PlayerInteractionHandler.OnPlayerInteracted -= OnInteractedWithSwitch;
    }

    private void OnInteractedWithSwitch()
    {
        switch(m_CurrentState)
        {
            case SwitchState.On:
                m_CurrentState = SwitchState.Off;
                ToggleLights(false);
                break;
            case SwitchState.Off:
                m_CurrentState = SwitchState.On;
                ToggleLights(true);
                OnLightsOn?.Invoke();
                break;
            case SwitchState.Unresponsive:
                break;
        }
    }

    private void ToggleLights(bool setActive)
    {
        SoundManager.OnPlaySoundEffects?.Invoke(SoundType.SwitchSound, false);

        foreach (Light lightSource in m_Lightsources)
        {
            lightSource.enabled = setActive;
        }
    }

    private void OnLigthsOff()
    {
        ToggleLights(false);
        m_CurrentState = SwitchState.Off;
    }

    public enum SwitchState
    {
        On,
        Off,
        Unresponsive
    }
}
