using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] protected int keysRequiredToTrigger;
    protected Collider triggerCollider;

    //Core Game Events
    public static Action OnLightsOffByGhost;
    public static Action OnSkullDrop;
    public static Action OnRatRush;
    public static Action OnPlayerEscaped;

    //Interactable Events
    public static Action<int> OnKeyPickedUp;
    public static Action OnPotionDrink;
    public static Action<bool> OnLightsSwitchToggled;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }
}
