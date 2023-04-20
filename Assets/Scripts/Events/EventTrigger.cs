using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] protected int m_KeysRequiredToTrigger;
    protected Collider triggerCollider;


    public static Action OnLightsOff;
    public static Action OnSkullDrop;
    public static Action OnRatRush;
    public static Action OnPlayerEscaped;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
        GetComponent<Collider>().isTrigger = true; // TODO - This thing is not needed, but I havnt removed this bcoz I dont know the reason
    }
}
