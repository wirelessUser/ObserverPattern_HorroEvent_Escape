using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractedEventTrigger : EventTrigger
{
    private bool Isinteracted;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Isinteracted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            UIManager.OnPlayerNearInteractable?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Interactable>() != null && Isinteracted)
        {
            Isinteracted = false;
            Debug.Log("Player Entered Interacted");
            other.GetComponent<Interactable>().Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            Debug.Log("Player Entered near Interactable");
            UIManager.OnPlayerNotNearInteractable?.Invoke();
        }
    }
}
