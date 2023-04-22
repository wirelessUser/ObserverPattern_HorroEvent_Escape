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
        EventManager.OnLightsOffByGhost += OnLightsOffByGhostEvent;
    }

    private void OnDisable()
    {
        EventManager.OnLightsOffByGhost -= OnLightsOffByGhostEvent;
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

        EventManager.Instance.InvokeOnLightsSwitchToggled(lights);

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

        EventManager.Instance.InvokeOnLightsSwitchToggled(lights);
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
        SoundManager.Instance.PlaySoundEffects(SoundType.SwitchSound, false);
        UIManager.instance.ShowInteractInstructions(false);
    }
    private void OnLightsOffByGhostEvent()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.SwitchSound, false);
        setLights(false);
    }
}
