using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // TODO - > Convert each Action into event Action , I have made EventManager as Singleton
    [SerializeField] protected int keysRequiredToTrigger;
    protected Collider triggerCollider;

    //Core Game Events
    public static Action OnLightsOffByGhost;
    public static Action OnSkullDrop;
    public static Action OnRatRush;
    public static Action OnPlayerEscaped;

    //Interactable Events
    public static Action<int> OnKeyPickedUp;
    public static event Action OnPotionDrink;
    public static Action<bool> OnLightsSwitchToggled;


    public static EventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("Event Manager instance already exists");
        }
        triggerCollider = GetComponent<Collider>();
    }


    public void InvokeOnPotionDrink()
    {
        OnPotionDrink?.Invoke();
    }


}
