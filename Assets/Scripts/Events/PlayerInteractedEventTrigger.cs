using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractedEventTrigger : EventTrigger
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnPlayerInteracted?.Invoke();
    }
}
