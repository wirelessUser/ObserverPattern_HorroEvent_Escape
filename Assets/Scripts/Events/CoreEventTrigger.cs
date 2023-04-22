using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEventTrigger : MonoBehaviour
{
    [SerializeField] protected int keysRequiredToTrigger;
    protected Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }
}
