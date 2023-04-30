using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, I_Interactable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    [SerializeField] private SoundService soundService;

    private SwitchState currentState;
    private void OnEnable()
    {
        EventService.Instance.LightSwitchToggleEvent.AddListener(OnLightsToggled);
        EventService.Instance.LightsOffByGhostEvent.AddListener(OnLightsOffByGhostEvent);
    }

    private void OnDisable()
    {
        EventService.Instance.LightSwitchToggleEvent.RemoveListener(OnLightsToggled);
        EventService.Instance.LightsOffByGhostEvent.RemoveListener(OnLightsOffByGhostEvent);
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
        Debug.Log("LightSwitch ToggleLights()");
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
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    private void SetLights(bool lights)
    {
        if (lights)
        {
            currentState = SwitchState.On;
        }
        else
        {
            currentState = SwitchState.Off;
        }

        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    public void Interact()
    {
        UIManager.Instance.ShowInteractInstructions(false);
        EventService.Instance.LightSwitchToggleEvent.InvokeEvent();
    }
    private void OnLightsOffByGhostEvent()
    {
        soundService.PlaySoundEffects(SoundType.SwitchSound);
        SetLights(false);
    }
    private void OnLightsToggled()
    {
        ToggleLights();
        soundService.PlaySoundEffects(SoundType.SwitchSound);
    }
}
