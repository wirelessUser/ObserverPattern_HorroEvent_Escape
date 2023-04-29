using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO -> This Entire Script is not needed , Move this into PlayerView
public class PlayerEventTrigger : MonoBehaviour //view
{
    private bool isInteracted;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracted = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            isInteracted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<I_Interactable>() != null)
        {
            UIManager.Instance.ShowInteractInstructions(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<I_Interactable>() != null && isInteracted)
        {
            isInteracted = false;
            Debug.Log("Player Entered Interacted");
            other.GetComponent<I_Interactable>().Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<I_Interactable>() != null)
        {
            Debug.Log("Player Entered near Interactable");
            UIManager.Instance.ShowInteractInstructions(false);
        }
    }
}
