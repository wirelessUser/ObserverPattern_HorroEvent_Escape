using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventTrigger : MonoBehaviour //view
{
    private bool Isinteracted; //naming

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
            UIManager.Instance.ShowInteractInstructions(true);
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
            UIManager.Instance.ShowInteractInstructions(false);
        }
    }
}
