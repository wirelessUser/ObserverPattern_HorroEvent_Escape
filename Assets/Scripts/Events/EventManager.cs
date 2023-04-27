using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO -> Delete This Mono , and Fix All Dependent Event Errors into EventService
public class EventManager : GenericMonoSingleton<EventManager>
{
    //Core Game Events
    public event Action OnLightsOffByGhost; // TODO - Remove Static from every action - use EventManager.Instance
    public static event Action OnSkullDrop;
    public static event Action OnRatRush;
    public static event Action OnPlayerEscaped;
    public static event Action OnPlayerDeath;

    //Interactable Events
    public static event Action<int> OnKeyPickedUp;
    public static event Action OnPotionDrink;
    public event Action<bool> OnLightsSwitchToggled;

    protected override void Awake()
    {
        base.Awake();
    }

    public void InvokeOnPotionDrink() => OnPotionDrink?.Invoke();

    public void InvokeOnSkullDrop() => OnSkullDrop?.Invoke();

    //TODO - Convert All these 1 line fuctions into 1 liners like above
    public void InvokeOnRatRush()
    {
        OnRatRush?.Invoke();
    }

    public void InvokeOnPlayerEscaped()
    {
        OnPlayerEscaped?.Invoke();
    }

    public void InvokeOnLightsOffByGhost()
    {
        OnLightsOffByGhost?.Invoke();
    }

    public void InvokeOnKeyPickedUp(int keysEquipped)
    {
        OnKeyPickedUp?.Invoke(keysEquipped);
    }

    public void InvokeOnLightsSwitchToggled(bool isSwitchOn)
    {
        OnLightsSwitchToggled?.Invoke(isSwitchOn);
    }

    public void InvokeOnPlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

}
