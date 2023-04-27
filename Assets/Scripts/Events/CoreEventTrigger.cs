using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo -> Remove This and Move Each and every event to EventService
public class CoreEventTrigger : MonoBehaviour
{
    [SerializeField] protected int keysRequiredToTrigger;
    protected Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }
}
