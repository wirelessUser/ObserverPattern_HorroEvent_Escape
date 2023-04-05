using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static Action OnKeyPickedUp;

    private void InteractedWithKey()
    {
        OnKeyPickedUp?.Invoke();
        PlayerController.OnPlayerInteracted -= InteractedWithKey;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            PlayerController.OnPlayerInteracted += InteractedWithKey;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            PlayerController.OnPlayerInteracted -= InteractedWithKey;
    }
}
