using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    public  delegate void ToggleLightDelegate();

    public static ToggleLightDelegate delegateLightToggle;
    private void Start() => currentState = SwitchState.Off;

    private void OnEnable()
    {
        delegateLightToggle = callLightSwitch;
        delegateLightToggle += SoundAndInstructions ;
    }
    public void Interact()
    {
        //Todo - Implement Interaction
        delegateLightToggle.Invoke();
    }
    private void toggleLights()
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
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    private void callLightSwitch()
    {
        toggleLights();
       
    }

    private void SoundAndInstructions()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }


}
