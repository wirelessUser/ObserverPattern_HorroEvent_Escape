using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    public static Action OnPlayerInteracted;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnPlayerInteracted?.Invoke();
    }
}
