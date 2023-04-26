using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightSwitch : Interactable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;
    private GameEvent lightSwitchEvent = new GameEvent();

    private void OnEnable()
    {
        EventManager.Instance.OnLightsOffByGhost += OnLightsOffByGhostEvent;
        // InteractableEvent += OnLightsToggeled;
        lightSwitchEvent.addListener(OnLightsToggeled);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnLightsOffByGhost -= OnLightsOffByGhostEvent;
        //  InteractableEvent -= OnLightsToggeled;
        lightSwitchEvent.removeListener(OnLightsToggeled);
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

        //EventManager.Instance.InvokeOnLightsSwitchToggled(lights);
        //InvokeInteractableEvent(lights);

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

        //  EventManager.Instance.InvokeOnLightsSwitchToggled(lights);
        // InvokeInteractableEvent();


        lightSwitchEvent.InvokeEvent();

        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    public override void Interact()
    {
        base.Interact();
        // InvokeInteractableEvent();
        lightSwitchEvent.InvokeEvent();

        Debug.Log("Light Switch Toggled");

    }
    private void OnLightsOffByGhostEvent()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.SwitchSound, false);
        setLights(false);
    }
    private void OnLightsToggeled()
    {
        ToggleLights();
        SoundManager.Instance.PlaySoundEffects(SoundType.SwitchSound, false);
    }
}
