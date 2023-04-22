using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightSwitch : Interactable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    private void OnEnable()
    {
        LightsOffEventTrigger.OnLightsOffByGhost += OnLightsOffByGhostEvent;
    }
    private void OnDisable()
    {
        LightsOffEventTrigger.OnLightsOffByGhost -= OnLightsOffByGhostEvent;
    }
    private void Start()
    {
        currentState = SwitchState.Off;
    }

    public enum SwitchState
    {
        On,
        Off,
        Unresponsive
    }

    private void ToggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }

        OnLightsSwitchToggled?.Invoke(lights);

        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    private void setLights(bool lights)
    {
        if (lights)
        {
            currentState = SwitchState.On;
        }
        else
        {
            currentState = SwitchState.Off;
        }

        OnLightsSwitchToggled?.Invoke(lights);
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Light Switch Toggled");
        ToggleLights();
        SoundManager.OnPlaySoundEffects?.Invoke(SoundType.SwitchSound, false);
        UIManager.OnPlayerNotNearInteractable?.Invoke();
    }
    private void OnLightsOffByGhostEvent()
    {
        SoundManager.OnPlaySoundEffects?.Invoke(SoundType.SwitchSound, false);
        setLights(false);
    }
}
