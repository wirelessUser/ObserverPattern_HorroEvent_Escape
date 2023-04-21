using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] protected int keysRequiredToTrigger;
    protected Collider triggerCollider;

    public static Action OnLightsOff;
    public static Action OnSkullDrop;
    public static Action OnRatRush;
    public static Action OnPlayerEscaped;
    public static Action OnPlayerInteracted;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }
}
