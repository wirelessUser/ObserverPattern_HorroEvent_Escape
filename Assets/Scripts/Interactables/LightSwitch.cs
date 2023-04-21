using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    public static Action OnLightsOn;

    private void Start()
    {
        currentState = SwitchState.Off;
        LightsOffEventTrigger.OnLightsOff += OnLigthsOff;
    }

    private void OnDisable()
    {
        LightsOffEventTrigger.OnLightsOff -= OnLigthsOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerInteractionHandler.OnPlayerInteracted += OnInteractedWithSwitch;
            UIManager.OnPlayerNearInteractable?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerInteractionHandler.OnPlayerInteracted -= OnInteractedWithSwitch;
            UIManager.OnPlayerNotNearInteractable?.Invoke();
        }
    }

    private void OnInteractedWithSwitch()
    {
        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                ToggleLights(false);
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
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

        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = setActive;
        }
    }

    private void OnLigthsOff()
    {
        ToggleLights(false);
        currentState = SwitchState.Off;
    }

    public enum SwitchState
    {
        On,
        Off,
        Unresponsive
    }
}
