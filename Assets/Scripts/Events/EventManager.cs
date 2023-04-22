using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : GenericMonoSingleton<EventManager>
{

    //Core Game Events
    public static event Action OnLightsOffByGhost;
    public static event Action OnSkullDrop;
    public static event Action OnRatRush;
    public static event Action OnPlayerEscaped;

    //Interactable Events
    public static event Action<int> OnKeyPickedUp;
    public static event Action OnPotionDrink;
    public static event Action<bool> OnLightsSwitchToggled;

    protected override void Awake()
    {
        base.Awake();
    }

    public void InvokeOnPotionDrink()
    {
        OnPotionDrink?.Invoke();
    }

    public void InvokeOnSkullDrop()
    {
        OnSkullDrop?.Invoke();
    }

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

}
